using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IBlogRepository
    {
        public Task<List<BlogVM>> getAllBlog(PaggingVM pagging);
        //public Task<List<BlogVM>> getNewBlog();
        public Task<List<BlogVM>> getAllUncheckBlog(PaggingVM pagging);
        public Task<Blog?> getBlogByBlogID(int id);
        public Task<Blog?> getBlogByBlogIDAndUserId(int blogId, int userId);
        //public Task<BlogVM> getBlogVMByBlogID(int id);
        public Task<List<BlogVM>> getBlogByBlogCateID(int blogCateID, PaggingVM pagging);
        //public Task<List<BlogVM>> getNewBlogByBlogCateID(int blogCateID);
        public Task<List<BlogVM>> getBlogByUserID(int userID, PaggingVM pagging);
        public Task<List<BlogVM>> getBlogBySelf(int userID, PaggingVM pagging);
        public Task<bool> CreateBlog(CreateBlogVM blogvm, int UserID);
        public Task<bool> UpdateBlog(Blog blog, UpdateBlogVM UpdatedBlogvm);
        public Task<bool> ConfirmBlog(Blog blog, int decision);
        public Task<bool> DenyBlog(Blog blog, int decision, String reason);
        public Task<int> getAllBlogCount();
        public Task<int> getAllBlogCount(int blogCateID);
        public Task<int> getBlogByUserIDCount(int userID);
        public Task<int> getBlogByBySelfCount(int userID);
    }
}
