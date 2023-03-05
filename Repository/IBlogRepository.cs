using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IBlogRepository
    {
        public List<Blog> getAllBlog();
        public List<BlogVM> getAllUncheckBlog();
        public Blog getBlogByBlogID(int id);
        public Blog getBlogByBlogIDAndUserId(int blogId, int userId);
        //public Task<BlogVM> getBlogVMByBlogID(int id);
        public List<BlogVM> getBlogByBlogCateID(int blogCateID);
        public List<BlogVM> getBlogByUserID(int userID);
        public List<BlogVM> getBlogBySelf(int userID);
        public bool CreateBlog(CreateBlogVM blogvm, int UserID);
        public bool UpdateBlog(Blog blog, UpdateBlogVM UpdatedBlogvm);
        public bool ConfirmBlog(Blog blog, int decision);
    }
}
