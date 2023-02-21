using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly MOBYContext context;
        public BlogRepository(MOBYContext context)
        {
            this.context = context;
        }

        public async Task<List<BlogVM>> getAllBlog()
        {
            List<BlogVM> blogList = new List<BlogVM>();
            blogList = context.Blogs.Select(b => BlogVM.BlogToVewModel(b)).ToList();

            return blogList;
        }
        public async Task<Blog> getBlogByBlogID(int id)
        {

            Blog blogFound = context.Blogs.Where(b => b.BlogId == id).FirstOrDefault();

            return blogFound;
        }

        public async Task<Blog> getBlogByBlogIDAndUserId(int blogId, int userId)
        {

            Blog blogFound = context.Blogs.Where(b => b.BlogId == blogId && b.UserId == userId).FirstOrDefault();

            return blogFound;
        }
        public async Task<BlogVM> getBlogVMByBlogID(int id)
        {

            BlogVM blogFound = context.Blogs.Where(b => b.BlogId == id).Select(b => BlogVM.BlogToVewModel(b)).FirstOrDefault();

            return blogFound;
        }
        public async Task<List<BlogVM>> getBlogByBlogCateID(int blogCateID)
        {
            List<BlogVM> blogList = new List<BlogVM>();
            blogList = context.Blogs.Where(b => b.BlogCategoryId == blogCateID).Select(b => BlogVM.BlogToVewModel(b)).ToList();

            return blogList;
        }
        public async Task<List<BlogVM>> getBlogByUserID(int userID)
        {
            List<BlogVM> blogList = new List<BlogVM>();
            blogList = context.Blogs.Where(b => b.UserId == userID).Select(b => BlogVM.BlogToVewModel(b)).ToList();

            return blogList;
        }

        public async Task<bool> CreateBlog(CreateBlogVM blogvm, int UserID)
        {
            try
            {
                Blog newblog = new Blog();
                newblog.BlogCategoryId = blogvm.BlogCategoryId;
                newblog.UserId = UserID;
                newblog.BlogTitle = blogvm.BlogTitle;
                newblog.BlogDescription = blogvm.BlogDescription;
                newblog.BlogContent = blogvm.BlogContent;
                newblog.BlogDateCreate = DateTime.Now;
                newblog.BlogStatus = true;
                context.Blogs.Add(newblog);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public async Task<bool> UpdateBlog(Blog blog, UpdateBlogVM blogvm)
        {
            try
            {

                blog.BlogCategoryId = blogvm.BlogCategoryId;

                blog.BlogTitle = blogvm.BlogTitle;
                blog.BlogDescription = blogvm.BlogDescription;
                blog.BlogContent = blogvm.BlogContent;
                blog.BlogDateUpdate = DateTime.Now;
                blog.BlogStatus = true;

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public async Task<bool> ConfirmBlog(Blog blog, bool decision)
        {
            try
            {


                blog.BlogStatus = decision;

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

    }
}
