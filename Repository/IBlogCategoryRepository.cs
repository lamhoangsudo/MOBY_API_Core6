using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Repository
{
    public interface IBlogCategoryRepository
    {
        public Task<List<BlogCategoryOnlyVM>> GetAllBlogCategory();
        public Task<bool> createBlogCategory(String name);
        public Task<bool> UpdateBlogCategory(UpdateBlogCategoryVM updateBlogCategoryVM);
        public Task<BlogCategoryVM?> GetBlogCateByID(int blogCateId);
    }
}
