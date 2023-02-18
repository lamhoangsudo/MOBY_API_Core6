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
            List<BlogCategoryVM> blogCategories = context.BlogCategories.Select(bc => BlogCategoryVM.BlogCategoryVMToVewModel(bc)).ToList(); ;

            return blogCategories;
        }

        public async Task<bool> createBlogCategory(String name)
        {
            BlogCategory newBlogCategory = new BlogCategory();
            newBlogCategory.BlogCategoryName = name;
            context.BlogCategories.Add(newBlogCategory);
            context.SaveChanges();
            return true;
        }
        public async Task<String> GetBlogCateNameByID(int blogCateId)
        {
            BlogCategory foundBlogCate = context.BlogCategories.Where(bc => bc.BlogCategoryId == blogCateId).FirstOrDefault();
            if (foundBlogCate != null)
            {
                return foundBlogCate.BlogCategoryName;
            }

            return "not found";
        }
        public async Task<bool> checkNameBlogCategory(String name)
        {
            BlogCategory foundBlogCate = context.BlogCategories.Where(bc => bc.BlogCategoryName == name).FirstOrDefault();
            if (foundBlogCate != null)
            {
                return true;
            }

            return false;
        }
    }
}
