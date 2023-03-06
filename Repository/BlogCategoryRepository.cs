using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class BlogCategoryRepository : IBlogCategoryRepository
    {
        private readonly MOBYContext context;

        public BlogCategoryRepository(MOBYContext context)
        {
            this.context = context;
        }
        public async Task<List<BlogCategoryVM>> GetAllBlogCategory()
        {
            List<BlogCategoryVM> blogCategories = await context.BlogCategories
                .Include(bc => bc.Blogs.Where(b => b.BlogStatus == 1))
                .Select(bc => BlogCategoryVM.BlogCategoryVMToVewModel(bc))
                .ToListAsync();

            return blogCategories;
        }

        public async Task<bool> createBlogCategory(String name)
        {
            BlogCategory? existBlogCate = await context.BlogCategories
                .Where(bc => bc.BlogCategoryName.Equals(name)).FirstOrDefaultAsync();
            if (existBlogCate != null)
            {
                return false;
            }
            BlogCategory newBlogCategory = new BlogCategory();
            newBlogCategory.BlogCategoryName = name;
            await context.BlogCategories.AddAsync(newBlogCategory);
            context.SaveChanges();
            return true;

        }
        public async Task<BlogCategoryVM?> GetBlogCateByID(int blogCateId)
        {
            BlogCategoryVM? foundBlogCate = await context.BlogCategories
                .Where(bc => bc.BlogCategoryId == blogCateId)
                .Include(bc => bc.Blogs.Where(b => b.BlogStatus == 1))
                .Select(bc => BlogCategoryVM.BlogCategoryVMToVewModel(bc))
                .FirstOrDefaultAsync();

            if (foundBlogCate != null)
            {
                return foundBlogCate;
            }
            else
            {
                return null;
            }
        }

    }
}
