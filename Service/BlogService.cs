using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class BlogService : IBlogService
    {
        private readonly IEmailService emailDAO;
        private readonly IBlogRepository blogRepository;
        public BlogService(IEmailService emailDAO, IBlogRepository blogRepository)
        {
            this.emailDAO = emailDAO;
            this.blogRepository = blogRepository;
        }
        public async Task<List<BlogSimpleVM>> GetAllBlog(PaggingVM pagging)
        {
            List<BlogSimpleVM> blogList = await blogRepository.GetAllBlog(pagging);
            return blogList;
        }
        public async Task<int> GetAllBlogCount()
        {
            return await blogRepository.GetAllBlogCount();
        }
        public async Task<List<BlogSimpleVM>> SearchlBlog(PaggingVM pagging, string tittle)
        {
            List<BlogSimpleVM> blogList = await blogRepository.SearchlBlog(pagging, tittle);
            return blogList;
        }
        public async Task<int> GetSearchBlogCount(string tittle)
        {
            return await blogRepository.GetSearchBlogCount(tittle);
        }
        public async Task<List<BlogBriefVM>> GetAllUncheckBlog(PaggingVM pagging, BlogStatusVM blogStatusVM)
        {
            List<BlogBriefVM> blogList = await blogRepository.GetAllUncheckBlog(pagging, blogStatusVM);
            return blogList;
        }
        public async Task<int> GetAllUncheckBlogcount(BlogStatusVM blogStatusVM)
        {
            return await blogRepository.GetAllUncheckBlogcount(blogStatusVM);
        }
        public async Task<Blog?> GetBlogByBlogID(int id)
        {

            Blog? blogFound = await blogRepository.GetBlogByBlogID(id);
            return blogFound;
        }
        public async Task<BlogVM?> GetBlogVMByBlogID(int id)
        {
            BlogVM? blogFound = await blogRepository.GetBlogVMByBlogID(id);
            return blogFound;
        }
        public async Task<Blog?> GetBlogByBlogIDAndUserId(int blogId, int userId)
        {

            Blog? blogFound = await blogRepository.GetBlogByBlogIDAndUserId(blogId, userId);
            return blogFound;
        }
        public async Task<List<BlogSimpleVM>> GetBlogByBlogCateID(int blogCateID, PaggingVM pagging)
        {
            List<BlogSimpleVM> blogList = await blogRepository.GetBlogByBlogCateID(blogCateID, pagging);
            return blogList;
        }
        public async Task<int> GetBlogByCateCount(int blogCateID)
        {
            return await blogRepository.GetBlogByCateCount(blogCateID);
        }
        public async Task<List<BlogSimpleVM>> GetBlogByUserID(int userID, PaggingVM pagging)
        {
            List<BlogSimpleVM> blogList = await blogRepository.GetBlogByUserID(userID, pagging);
            return blogList;
        }
        public async Task<int> GetBlogByUserIDCount(int userID)
        {
            return await blogRepository.GetBlogByUserIDCount(userID);
        }
        public async Task<List<BlogSimpleVM>> GetBlogBySelf(int userID, PaggingVM pagging)
        {
            List<BlogSimpleVM> blogList = await blogRepository.GetBlogBySelf(userID, pagging);
            return blogList;
        }
        public async Task<int> GetBlogByBySelfCount(int userID)
        {
            return await blogRepository.GetBlogByBySelfCount(userID);
        }
        public async Task<bool> CreateBlog(CreateBlogVM blogvm, int UserID)
        {
            int check = await blogRepository.CreateBlog(blogvm, UserID);
            if (check != 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateBlog(Blog blog, UpdateBlogVM blogvm)
        {
            int check = await blogRepository.UpdateBlog(blog, blogvm);
            if (check != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> ConfirmBlog(Blog blog, int decision)
        {
            int check = await blogRepository.ConfirmBlog(blog, decision);
            if (check == 0)
            {
                return false;
            }
            Email newEmail = new()
            {
                To = blog.User.UserGmail,
                UserName = blog.User.UserName,
                Subject = "Bài Blog của bạn đã được phê duyệt",
                Obj = "Blog",
                Link = "https://moby-customer.vercel.app/blog/" + blog.BlogId
            };
            await emailDAO.SendEmai(newEmail);
            return true;
        }
        public async Task<bool> DenyBlog(Blog blog, string reason)
        {
            int check = await blogRepository.DenyBlog(blog, reason);
            if (check == 0)
            {
                return false;
            }
            Email newEmail = new()
            {
                To = blog.User.UserGmail,
                Subject = "Bài Blog của bạn đã bị từ chối",
                UserName = blog.User.UserName,
                Obj = "Blog",
                Link = "https://moby-customer.vercel.app/blog/" + blog.BlogId
            };
            await emailDAO.SendEmai(newEmail);
            return true;
        }
    }
}
