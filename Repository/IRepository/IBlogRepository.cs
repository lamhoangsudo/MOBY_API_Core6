using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository.IRepository
{
    public interface IBlogRepository
    {
        Task<List<BlogSimpleVM>> GetAllBlog(PaggingVM pagging);
        Task<int> GetAllBlogCount();
        Task<List<BlogSimpleVM>> SearchlBlog(PaggingVM pagging, string tittle);
        Task<int> GetSearchBlogCount(string tittle);
        Task<List<BlogBriefVM>> GetAllUncheckBlog(PaggingVM pagging, BlogStatusVM blogStatusVM);
        Task<int> GetAllUncheckBlogcount(BlogStatusVM blogStatusVM);
        Task<Blog?> GetBlogByBlogID(int id);
        Task<BlogVM?> GetBlogVMByBlogID(int id);
        Task<Blog?> GetBlogByBlogIDAndUserId(int blogId, int userId);
        Task<List<BlogSimpleVM>> GetBlogByBlogCateID(int blogCateID, PaggingVM pagging);
        Task<int> GetBlogByCateCount(int blogCateID);
        Task<List<BlogSimpleVM>> GetBlogByUserID(int userID, PaggingVM pagging);
        Task<int> GetBlogByUserIDCount(int userID);
        Task<List<BlogSimpleVM>> GetBlogBySelf(int userID, PaggingVM pagging);
        Task<int> GetBlogByBySelfCount(int userID);
        Task<int> CreateBlog(CreateBlogVM blogvm, int UserID);
        Task<int> UpdateBlog(Blog blog, UpdateBlogVM blogvm);
        Task<int> ConfirmBlog(Blog blog, int decision);
        Task<int> DenyBlog(Blog blog, string reason);
    }
}
