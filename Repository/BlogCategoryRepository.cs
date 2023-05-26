using Microsoft.EntityFrameworkCore;
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
                .Where(bc => bc.Status == true)
                .Select(bc => BlogCategoryOnlyVM.BlogCategoryToVewModel(bc))
                .ToListAsync();
            }
            if (status == 1)
            {
                return await context.BlogCategories
                .Where(bc => bc.Status == false)
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
                    Status = true
                };
                await context.BlogCategories.AddAsync(newBlogCategory);
                await context.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
        public async Task<int> UpdateBlogCategory(UpdateBlogCategoryVM updateBlogCategoryVM)
        {
            BlogCategory? existBlogCate = await context.BlogCategories
                .Where(bc => bc.BlogCategoryId == updateBlogCategoryVM.BlogCategoryId).FirstOrDefaultAsync();
            if (existBlogCate != null)
            {
                existBlogCate.BlogCategoryName = updateBlogCategoryVM.BlogCategoryName;
                existBlogCate.Status = true;
                await context.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
        public async Task<int> DeleteBlogCategory(BlogCateGetVM blogCateGetVM)
        {
            BlogCategory? existBlogCate = await context.BlogCategories
                .Where(bc => bc.BlogCategoryId == blogCateGetVM.BlogCategoryId).FirstOrDefaultAsync();
            if (existBlogCate != null)
            {
                existBlogCate.Status = false;
                await context.SaveChangesAsync();
                return 1;
            }
            return 0;
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
