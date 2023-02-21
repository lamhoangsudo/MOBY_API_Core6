using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IBlogRepository
    {
        public Task<List<BlogVM>> getAllBlog();
        public Task<Blog> getBlogByBlogID(int id);
        public Task<BlogVM> getBlogVMByBlogID(int id);
        public Task<List<BlogVM>> getBlogByBlogCateID(int blogCateID);
        public Task<List<BlogVM>> getBlogByUserID(int userID);
        public Task<bool> CreateBlog(CreateBlogVM blogvm, int UserID);
        public Task<bool> UpdateBlog(Blog blog, UpdateBlogVM UpdatedBlogvm);
        public Task<bool> ConfirmBlog(Blog blog, bool decision);
    }
}
