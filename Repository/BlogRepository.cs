using Microsoft.EntityFrameworkCore;
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

        public List<Blog> getAllBlog()
        {
            List<Blog> blogList = new List<Blog>();
            //blogList = context.Blogs.Select(b => BlogVM.BlogToVewModel(b)).ToList();
            blogList = context.Blogs.Include(b => b.Comments).ThenInclude(cmt => cmt.Replies).ToList();
            /*foreach (var blog in blogList)
            {
                blog.Comments = context.Comments.Where(cmt => cmt.BlogId == blog.BlogId).Include(cmt => cmt.Replies).ToList();
            }*/
            return blogList;
        }

        public List<BlogVM> getAllUncheckBlog()
        {
            List<BlogVM> blogList = new List<BlogVM>();
            //blogList = context.Blogs.Select(b => BlogVM.BlogToVewModel(b)).ToList();
            blogList = context.Blogs.Where(b => b.BlogStatus == 0).Select(b => BlogVM.BlogToVewModel(b)).ToList();
            /*foreach (var blog in blogList)
            {
                blog.Comments = context.Comments.Where(cmt => cmt.BlogId == blog.BlogId).Include(cmt => cmt.Replies).ToList();
            }*/
            return blogList;
        }
        public Blog getBlogByBlogID(int id)
        {

            Blog blogFound = context.Blogs.Where(b => b.BlogId == id).Include(b => b.Comments).ThenInclude(cmt => cmt.Replies).FirstOrDefault();

            return blogFound;
        }

        public Blog getBlogByBlogIDAndUserId(int blogId, int userId)
        {

            Blog blogFound = context.Blogs.Where(b => b.BlogId == blogId && b.UserId == userId).FirstOrDefault();

            return blogFound;
        }
        /*public async Task<BlogVM> getBlogVMByBlogID(int id)
        {

            BlogVM blogFound = context.Blogs.Where(b => b.BlogId == id).Select(b => BlogVM.BlogToVewModel(b)).FirstOrDefault();

            return blogFound;
        }*/
        public List<BlogVM> getBlogByBlogCateID(int blogCateID)
        {
            List<BlogVM> blogList = new List<BlogVM>();
            blogList = context.Blogs.Where(b => b.BlogCategoryId == blogCateID && b.BlogStatus == 1).Select(b => BlogVM.BlogToVewModel(b)).ToList();

            return blogList;
        }
        public List<BlogVM> getBlogByUserID(int userID)
        {
            List<BlogVM> blogList = new List<BlogVM>();
            blogList = context.Blogs.Where(b => b.UserId == userID && b.BlogStatus == 1).Select(b => BlogVM.BlogToVewModel(b)).ToList();

            return blogList;
        }

        public List<BlogVM> getBlogBySelf(int userID)
        {
            List<BlogVM> blogList = new List<BlogVM>();
            blogList = context.Blogs.Where(b => b.UserId == userID && b.BlogStatus != 3).Select(b => BlogVM.BlogToVewModel(b)).ToList();

            return blogList;
        }

        public bool CreateBlog(CreateBlogVM blogvm, int UserID)
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
                newblog.BlogStatus = 0;
                context.Blogs.Add(newblog);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public bool UpdateBlog(Blog blog, UpdateBlogVM blogvm)
        {

            if (blog.BlogStatus != 3)
            {
                blog.BlogCategoryId = blogvm.BlogCategoryId;
                blog.BlogTitle = blogvm.BlogTitle;
                blog.BlogDescription = blogvm.BlogDescription;
                blog.BlogContent = blogvm.BlogContent;
                blog.BlogDateUpdate = DateTime.Now;
                if (blog.BlogStatus == 1 || blog.BlogStatus == 2)
                {
                    blog.BlogStatus = 0;
                }
                if (context.SaveChanges() != 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }


        }
        public bool ConfirmBlog(Blog blog, int decision)
        {
            blog.BlogStatus = decision;
            if (context.SaveChanges() != 0)
            {
                return true;
            }

            return false;
        }

    }
}
