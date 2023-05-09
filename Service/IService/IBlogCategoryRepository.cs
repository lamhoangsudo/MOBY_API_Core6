using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Service.IService
{
    public interface IBlogCategoryRepository
    {
        public Task<List<BlogCategoryOnlyVM>> GetAllBlogCategory(int status);
        public Task<int> CreateBlogCategory(string name);
        public Task<int> UpdateBlogCategory(UpdateBlogCategoryVM updateBlogCategoryVM);
        public Task<BlogCategoryVM?> GetBlogCateByID(int blogCateId);
        public Task<int> DeleteBlogCategory(BlogCateGetVM blogCateGetVM);
    }
}
