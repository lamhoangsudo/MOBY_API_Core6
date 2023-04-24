using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly MOBYContext context;
        private readonly IEmailRepository emailDAO;
        public BlogRepository(MOBYContext context, IEmailRepository emailDAO)
        {
            this.context = context;
            this.emailDAO = emailDAO;
        }

        public async Task<List<BlogSimpleVM>> getAllBlog(PaggingVM pagging)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<BlogSimpleVM> blogList = new List<BlogSimpleVM>();
            if (pagging.orderBy)
            {
                blogList = await context.Blogs
                .Include(b => b.BlogCategory)
                .Include(b => b.User)
                .Where(b => b.BlogStatus == 1)
                .OrderByDescending(b => b.BlogId)
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();

            }
            else
            {
                blogList = await context.Blogs
                .Include(b => b.BlogCategory)
                .Include(b => b.User)
                .Where(b => b.BlogStatus == 1)
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();

            }
            return blogList;
        }
        public async Task<int> getAllBlogCount()
        {
            int count;
            count = await context.Blogs.Where(b => b.BlogStatus == 1)
                .CountAsync();

            return count;
        }

        public async Task<List<BlogSimpleVM>> SearchlBlog(PaggingVM pagging, string tittle)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<BlogSimpleVM> blogList = new List<BlogSimpleVM>();
            if (pagging.orderBy)
            {
                blogList = await context.Blogs
                .Include(b => b.BlogCategory)
                .Include(b => b.User)
                .Where(b => b.BlogStatus == 1 && b.BlogTitle.Contains(tittle))
                .OrderByDescending(b => b.BlogId)
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();

            }
            else
            {
                blogList = await context.Blogs
                .Include(b => b.BlogCategory)
                .Include(b => b.User)
                .Where(b => b.BlogStatus == 1 && b.BlogTitle.Contains(tittle))
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();
            }

            return blogList;
        }

        public async Task<int> getSearchBlogCount(string tittle)
        {
            int count;
            count = await context.Blogs.Where(b => b.BlogStatus == 1 && b.BlogTitle.Contains(tittle))
                .CountAsync();

            return count;
        }


        public async Task<List<BlogBriefVM>> getAllUncheckBlog(PaggingVM pagging, BlogStatusVM blogStatusVM)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<BlogBriefVM> blogList = new List<BlogBriefVM>();
            if (blogStatusVM.BlogStatus == null)
            {
                blogList = await context.Blogs
               .Include(b => b.User)
               .Skip(itemsToSkip)
               .Take(pagging.pageSize)
               .Select(b => BlogBriefVM.BlogBriefToVewModel(b))
               .ToListAsync();

                return blogList;
            }
            blogList = await context.Blogs
               .Include(b => b.User)
               .Where(b => b.BlogStatus == blogStatusVM.BlogStatus)
               .Skip(itemsToSkip)
               .Take(pagging.pageSize)
               .Select(b => BlogBriefVM.BlogBriefToVewModel(b))
               .ToListAsync();

            return blogList;
        }
        public async Task<int> getAllUncheckBlogcount(BlogStatusVM blogStatusVM)
        {
            int blogListcount;
            if (blogStatusVM.BlogStatus == null)
            {
                blogListcount = await context.Blogs.CountAsync();
            }
            blogListcount = await context.Blogs.Where(b => b.BlogStatus == blogStatusVM.BlogStatus).CountAsync();

            return blogListcount;
        }

        public async Task<Blog?> getBlogByBlogID(int id)
        {

            Blog? blogFound = await context.Blogs.Where(b => b.BlogId == id)
                .Include(b => b.User)
                .FirstOrDefaultAsync();

            return blogFound;
        }

        public async Task<BlogVM?> getBlogVMByBlogID(int id)
        {

            BlogVM? blogFound = await context.Blogs.Where(b => b.BlogId == id)
                .Include(b => b.BlogCategory)
                .Include(b => b.User)
                .Include(b => b.Comments)
                .ThenInclude(c => c.User)
                .Include(b => b.Comments)
                .ThenInclude(cmt => cmt.Replies)
                .ThenInclude(rep => rep.User)
                .Select(b => BlogVM.BlogToVewModel(b))
                .FirstOrDefaultAsync();

            return blogFound;
        }

        public async Task<Blog?> getBlogByBlogIDAndUserId(int blogId, int userId)
        {

            Blog? blogFound = await context.Blogs
                .Where(b => b.BlogId == blogId && b.UserId == userId)
                .FirstOrDefaultAsync();

            return blogFound;
        }

        public async Task<List<BlogSimpleVM>> getBlogByBlogCateID(int blogCateID, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<BlogSimpleVM> blogList = new List<BlogSimpleVM>();
            if (pagging.orderBy)
            {
                blogList = await context.Blogs.Where(b => b.BlogCategoryId == blogCateID && b.BlogStatus == 1)
                .Include(b => b.BlogCategory)
                .Include(b => b.User)
                .OrderByDescending(b => b.BlogId)
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();
            }
            else
            {
                blogList = await context.Blogs.Where(b => b.BlogCategoryId == blogCateID && b.BlogStatus == 1)
                .Include(b => b.BlogCategory)
                .Include(b => b.User)
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();
            }

            return blogList;
        }
        public async Task<int> getBlogByCateCount(int blogCateID)
        {
            int count;
            count = await context.Blogs.Where(b => b.BlogStatus == 1 && b.BlogCategoryId == blogCateID)
                .CountAsync();

            return count;
        }
        public async Task<List<BlogSimpleVM>> getBlogByUserID(int userID, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<BlogSimpleVM> blogList = new List<BlogSimpleVM>();
            if (pagging.orderBy)
            {

                blogList = await context.Blogs.Where(b => b.UserId == userID && b.BlogStatus == 1)
                    .Include(b => b.BlogCategory)
                    .Include(b => b.User)
                    .Skip(itemsToSkip)
                    .Take(pagging.pageSize)
                    .OrderByDescending(b => b.BlogId)
                    .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                    .ToListAsync();
            }
            else
            {
                blogList = await context.Blogs.Where(b => b.UserId == userID && b.BlogStatus == 1)
                    .Include(b => b.BlogCategory)
                    .Include(b => b.User)
                    .Skip(itemsToSkip)
                    .Take(pagging.pageSize)
                    .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                    .ToListAsync();
            }

            return blogList;
        }
        public async Task<int> getBlogByUserIDCount(int userID)
        {
            int count;
            count = await context.Blogs.Where(b => b.UserId == userID && b.BlogStatus == 1)
                .CountAsync();

            return count;
        }

        public async Task<List<BlogSimpleVM>> getBlogBySelf(int userID, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<BlogSimpleVM> blogList = new List<BlogSimpleVM>();
            if (pagging.orderBy)
            {
                blogList = await context.Blogs.Where(b => b.UserId == userID && b.BlogStatus != 3)
                .Include(b => b.User)
                .Include(b => b.BlogCategory)
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .OrderByDescending(b => b.BlogId)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();
            }
            else
            {
                blogList = await context.Blogs.Where(b => b.UserId == userID && b.BlogStatus != 3)
                .Include(b => b.User)
                .Include(b => b.BlogCategory)
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();
            }

            return blogList;
        }

        public async Task<int> getBlogByBySelfCount(int userID)
        {
            int count;
            count = await context.Blogs.Where(b => b.UserId == userID && b.BlogStatus != 3)
                .CountAsync();

            return count;
        }

        public async Task<bool> CreateBlog(CreateBlogVM blogvm, int UserID)
        {

            Blog newblog = new Blog();
            newblog.BlogCategoryId = blogvm.BlogCategoryId;
            newblog.UserId = UserID;
            newblog.BlogTitle = blogvm.BlogTitle;
            newblog.Image = blogvm.Image;
            newblog.BlogDescription = blogvm.BlogDescription;
            newblog.BlogContent = blogvm.BlogContent;
            newblog.BlogDateCreate = DateTime.Now;
            newblog.BlogStatus = 0;
            await context.Blogs.AddAsync(newblog);
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;

        }

        public async Task<bool> UpdateBlog(Blog blog, UpdateBlogVM blogvm)
        {

            if (blog.BlogStatus != 3)
            {
                blog.BlogCategoryId = blogvm.BlogCategoryId;
                blog.BlogTitle = blogvm.BlogTitle;
                blog.Image = blogvm.Image;
                blog.BlogDescription = blogvm.BlogDescription;
                blog.BlogContent = blogvm.BlogContent;
                blog.BlogDateUpdate = DateTime.Now;
                if (blog.BlogStatus == 1 || blog.BlogStatus == 2)
                {
                    blog.BlogStatus = 0;
                    blog.ReasonDeny = null;
                }
                await context.SaveChangesAsync();
                return true;


            }
            else
            {
                return false;
            }


        }
        public async Task<bool> ConfirmBlog(Blog blog, int decision)
        {
            blog.BlogStatus = decision;
            blog.ReasonDeny = null;
            await context.SaveChangesAsync();
            if (decision == 1)
            {
                Email newEmail = new Email();
                newEmail.To = blog.User.UserGmail;
                newEmail.Subject = "your blog has been accepted";
                newEmail.Body = "https://moby-customer.vercel.app/blog/" + blog.BlogId + " has been accepted by admintrator";
                await emailDAO.SendEmai(newEmail);

            }
            return true;



        }

        public async Task<bool> DenyBlog(Blog blog, String reason)
        {
            blog.BlogStatus = 2;
            blog.ReasonDeny = reason;
            await context.SaveChangesAsync();
            Email newEmail = new Email();
            newEmail.To = blog.User.UserGmail;
            newEmail.Subject = "your blog has been denied";
            newEmail.Body = "https://moby-customer.vercel.app/blog/" + blog.BlogId + " has been denied by admintrator /n reason: " + reason;
            await emailDAO.SendEmai(newEmail);
            return true;



        }

    }
}
