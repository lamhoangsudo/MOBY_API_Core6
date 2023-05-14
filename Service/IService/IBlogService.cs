using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Service.IService
{
    public interface IBlogService
    {
        public Task<List<BlogSimpleVM>> GetAllBlog(PaggingVM pagging);
        //public Task<List<BlogVM>> getNewBlog();
        public Task<List<BlogSimpleVM>> SearchlBlog(PaggingVM pagging, string tittle);
        public Task<int> GetSearchBlogCount(string tittle);
        public Task<List<BlogBriefVM>> GetAllUncheckBlog(PaggingVM pagging, BlogStatusVM blogStatusVM);
        public Task<int> GetAllUncheckBlogcount(BlogStatusVM blogStatusVM);
        public Task<Blog?> GetBlogByBlogID(int id);
        public Task<BlogVM?> GetBlogVMByBlogID(int id);
        public Task<Blog?> GetBlogByBlogIDAndUserId(int blogId, int userId);
        //public Task<BlogVM> getBlogVMByBlogID(int id);
        public Task<List<BlogSimpleVM>> GetBlogByBlogCateID(int blogCateID, PaggingVM pagging);
        //public Task<List<BlogVM>> getNewBlogByBlogCateID(int blogCateID);
        public Task<List<BlogSimpleVM>> GetBlogByUserID(int userID, PaggingVM pagging);
        public Task<List<BlogSimpleVM>> GetBlogBySelf(int userID, PaggingVM pagging);
        public Task<bool> CreateBlog(CreateBlogVM blogvm, int UserID);
        public Task<bool> UpdateBlog(Blog blog, UpdateBlogVM UpdatedBlogvm);
        public Task<bool> ConfirmBlog(Blog blog, int decision);
        public Task<bool> DenyBlog(Blog blog, string reason);
        public Task<int> GetAllBlogCount();
        public Task<int> GetBlogByCateCount(int blogCateID);
        public Task<int> GetBlogByUserIDCount(int userID);
        public Task<int> GetBlogByBySelfCount(int userID);
    }
}
