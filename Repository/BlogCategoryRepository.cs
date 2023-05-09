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
        //done
        public async Task<List<BlogCategoryOnlyVM>> GetAllBlogCategory(int status)
        {
            List<BlogCategoryOnlyVM> blogCategories = new List<BlogCategoryOnlyVM>();
            if (status == 0)
            {
                blogCategories = await context.BlogCategories
                .Where(bc => bc.Status == null || bc.Status == "ennable")
                .Select(bc => BlogCategoryOnlyVM.BlogCategoryToVewModel(bc))
                .ToListAsync();

            }
            else
            {
                blogCategories = await context.BlogCategories
                .Select(bc => BlogCategoryOnlyVM.BlogCategoryToVewModel(bc))
                .ToListAsync();

            }
            return blogCategories;
        }
        //done
        public async Task<bool> CreateBlogCategory(String name)
        {
            BlogCategory? existBlogCate = await context.BlogCategories
                .Where(bc => bc.BlogCategoryName.Equals(name)).FirstOrDefaultAsync();
            if (existBlogCate != null)
            {
                return false;
            }
            BlogCategory newBlogCategory = new BlogCategory();
            newBlogCategory.BlogCategoryName = name;
            newBlogCategory.Status = "ennable";
            await context.BlogCategories.AddAsync(newBlogCategory);
            context.SaveChanges();
            return true;

        }
        //done
        public async Task<bool> UpdateBlogCategory(UpdateBlogCategoryVM updateBlogCategoryVM)
        {
            BlogCategory? existBlogCate = await context.BlogCategories
                .Where(bc => bc.BlogCategoryId == updateBlogCategoryVM.BlogCategoryId).FirstOrDefaultAsync();
            if (existBlogCate == null)
            {
                return false;
            }
            existBlogCate.BlogCategoryName = updateBlogCategoryVM.BlogCategoryName;
            existBlogCate.Status = "ennable";
            await context.SaveChangesAsync();
            return true;

        }
        //done
        public async Task<bool> DeleteBlogCategory(BlogCateGetVM blogCateGetVM)
        {
            BlogCategory? existBlogCate = await context.BlogCategories
                .Where(bc => bc.BlogCategoryId == blogCateGetVM.BlogCategoryId).FirstOrDefaultAsync();
            if (existBlogCate == null)
            {
                return false;
            }

            existBlogCate.Status = "disable";
            await context.SaveChangesAsync();
            return true;

        }
        //done
        public async Task<BlogCategoryVM?> GetBlogCateByID(int blogCateId)
        {
            BlogCategoryVM? foundBlogCate = await context.BlogCategories
                .Where(bc => bc.BlogCategoryId == blogCateId && (bc.Status == null || bc.Status == "ennable"))
                .Include(bc => bc.Blogs.Where(b => b.BlogStatus == 1))
                .ThenInclude(b => b.User)
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
