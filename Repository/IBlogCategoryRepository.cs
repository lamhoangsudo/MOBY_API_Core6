using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Repository
{
    public interface IBlogCategoryRepository
    {
        public Task<List<BlogCategoryVM>> GetAllBlogCategory();
        public Task<bool> createBlogCategory(String name);
        public Task<bool> checkNameBlogCategory(String name);
        public Task<String> GetBlogCateNameByID(int blogCateId);
    }
}
