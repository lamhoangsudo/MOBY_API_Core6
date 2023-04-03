using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IBlogRepository
    {
        public Task<List<BlogSimpleVM>> getAllBlog(PaggingVM pagging);
        //public Task<List<BlogVM>> getNewBlog();
        public Task<List<BlogSimpleVM>> SearchlBlog(PaggingVM pagging, string tittle);
        public Task<int> getSearchBlogCount(string tittle);
        public Task<List<BlogBriefVM>> getAllUncheckBlog(PaggingVM pagging, BlogStatusVM blogStatusVM);
        public Task<int> getAllUncheckBlogcount(BlogStatusVM blogStatusVM);
        public Task<Blog?> getBlogByBlogID(int id);
        public Task<BlogVM?> getBlogVMByBlogID(int id);
        public Task<Blog?> getBlogByBlogIDAndUserId(int blogId, int userId);
        //public Task<BlogVM> getBlogVMByBlogID(int id);
        public Task<List<BlogSimpleVM>> getBlogByBlogCateID(int blogCateID, PaggingVM pagging);
        //public Task<List<BlogVM>> getNewBlogByBlogCateID(int blogCateID);
        public Task<List<BlogSimpleVM>> getBlogByUserID(int userID, PaggingVM pagging);
        public Task<List<BlogSimpleVM>> getBlogBySelf(int userID, PaggingVM pagging);
        public Task<bool> CreateBlog(CreateBlogVM blogvm, int UserID);
        public Task<bool> UpdateBlog(Blog blog, UpdateBlogVM UpdatedBlogvm);
        public Task<bool> ConfirmBlog(Blog blog, int decision);
        public Task<bool> DenyBlog(Blog blog, int decision, String reason);
        public Task<int> getAllBlogCount();
        public Task<int> getBlogByCateCount(int blogCateID);
        public Task<int> getBlogByUserIDCount(int userID);
        public Task<int> getBlogByBySelfCount(int userID);
    }
}
