﻿using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Repository
{
    public class BlogCategoryRepository : IBlogCategoryRepository
    {
        private readonly MOBYContext context;
        public BlogCategoryRepository(MOBYContext context)
        {
            this.context = context;
        }
        public async Task<List<BlogCategoryOnlyVM>> GetAllBlogCategory(int status)
        {
            if (status == 0)
            {
                return await context.BlogCategories
                .Where(bc => bc.Status == null || bc.Status == "ennable")
                .Select(bc => BlogCategoryOnlyVM.BlogCategoryToVewModel(bc))
                .ToListAsync();
            }
            else
            {
                return await context.BlogCategories
                .Select(bc => BlogCategoryOnlyVM.BlogCategoryToVewModel(bc))
                .ToListAsync();
            }
            throw new InvalidDataException();
        }
        public async Task<int> CreateBlogCategory(string name)
        {
            BlogCategory? existBlogCate = await context.BlogCategories
                .Where(bc => bc.BlogCategoryName.Equals(name)).FirstOrDefaultAsync();
            if (existBlogCate == null)
            {
                BlogCategory newBlogCategory = new()
                {
                    BlogCategoryName = name,
                    Status = "ennable"
                };
                await context.BlogCategories.AddAsync(newBlogCategory);
                return await context.SaveChangesAsync();
            }
            throw new DuplicateWaitObjectException();
        }
        public async Task<int> UpdateBlogCategory(UpdateBlogCategoryVM updateBlogCategoryVM)
        {
            BlogCategory? existBlogCate = await context.BlogCategories
                .Where(bc => bc.BlogCategoryId == updateBlogCategoryVM.BlogCategoryId).FirstOrDefaultAsync();
            if (existBlogCate != null)
            {
                existBlogCate.BlogCategoryName = updateBlogCategoryVM.BlogCategoryName;
                existBlogCate.Status = "ennable";
                return await context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<int> DeleteBlogCategory(BlogCateGetVM blogCateGetVM)
        {
            BlogCategory? existBlogCate = await context.BlogCategories
                .Where(bc => bc.BlogCategoryId == blogCateGetVM.BlogCategoryId).FirstOrDefaultAsync();
            if (existBlogCate != null)
            {
                existBlogCate.Status = "disable";
                return await context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<BlogCategoryVM?> GetBlogCateByID(int blogCateId)
        {
            return await context.BlogCategories
                .Where(bc => bc.BlogCategoryId == blogCateId && (bc.Status == null || bc.Status == "ennable"))
                .Include(bc => bc.Blogs.Where(b => b.BlogStatus == 1))
                .ThenInclude(b => b.User)
                .Select(bc => BlogCategoryVM.BlogCategoryVMToVewModel(bc))
                .FirstOrDefaultAsync();
        }
    }
}
