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
        //done
        public async Task<List<BlogSimpleVM>> GetAllBlog(PaggingVM pagging)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            List<BlogSimpleVM> blogList = new();
            if (pagging.OrderBy)
            {
                blogList = await context.Blogs
                .Include(b => b.BlogCategory)
                .Include(b => b.User)
                .Where(b => b.BlogStatus == 1)
                .OrderByDescending(b => b.BlogId)
                .Skip(itemsToSkip)
                .Take(pagging.PageSize)
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
                .Take(pagging.PageSize)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();

            }
            return blogList;
        }
        //done
        public async Task<int> GetAllBlogCount()
        {
            int count;
            count = await context.Blogs
                .Where(b => b.BlogStatus == 1)
                .CountAsync();
            return count;
        }
        //done
        public async Task<List<BlogSimpleVM>> SearchlBlog(PaggingVM pagging, string tittle)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            List<BlogSimpleVM> blogList = new();
            if (pagging.OrderBy)
            {
                blogList = await context.Blogs
                .Include(b => b.BlogCategory)
                .Include(b => b.User)
                .Where(b => b.BlogStatus == 1 && b.BlogTitle.Contains(tittle))
                .OrderByDescending(b => b.BlogId)
                .Skip(itemsToSkip)
                .Take(pagging.PageSize)
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
                .Take(pagging.PageSize)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();
            }

            return blogList;
        }
        //done
        public async Task<int> GetSearchBlogCount(string tittle)
        {
            int count;
            count = await context.Blogs.Where(b => b.BlogStatus == 1 && b.BlogTitle.Contains(tittle))
                .CountAsync();
            return count;
        }
        //done
        public async Task<List<BlogBriefVM>> GetAllUncheckBlog(PaggingVM pagging, BlogStatusVM blogStatusVM)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            List<BlogBriefVM> blogList = new();
            if (blogStatusVM.BlogStatus == null)
            {
                blogList = await context.Blogs
               .Include(b => b.User)
               .Skip(itemsToSkip)
               .Take(pagging.PageSize)
               .Select(b => BlogBriefVM.BlogBriefToVewModel(b))
               .ToListAsync();

                return blogList;
            }
            blogList = await context.Blogs
               .Include(b => b.User)
               .Where(b => b.BlogStatus == blogStatusVM.BlogStatus)
               .Skip(itemsToSkip)
               .Take(pagging.PageSize)
               .Select(b => BlogBriefVM.BlogBriefToVewModel(b))
               .ToListAsync();

            return blogList;
        }
        //done
        public async Task<int> GetAllUncheckBlogcount(BlogStatusVM blogStatusVM)
        {
            int blogListcount;
            if (blogStatusVM.BlogStatus == null)
            {
                blogListcount = await context.Blogs.CountAsync();
            }
            blogListcount = await context.Blogs.Where(b => b.BlogStatus == blogStatusVM.BlogStatus).CountAsync();

            return blogListcount;
        }
        //done
        public async Task<Blog?> GetBlogByBlogID(int id)
        {

            Blog? blogFound = await context.Blogs.Where(b => b.BlogId == id)
                .Include(b => b.User)
                .FirstOrDefaultAsync();

            return blogFound;
        }
        //done
        public async Task<BlogVM?> GetBlogVMByBlogID(int id)
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
        //done
        public async Task<Blog?> GetBlogByBlogIDAndUserId(int blogId, int userId)
        {

            Blog? blogFound = await context.Blogs
                .Where(b => b.BlogId == blogId && b.UserId == userId)
                .FirstOrDefaultAsync();

            return blogFound;
        }
        //done
        public async Task<List<BlogSimpleVM>> GetBlogByBlogCateID(int blogCateID, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            List<BlogSimpleVM> blogList = new();
            if (pagging.OrderBy)
            {
                blogList = await context.Blogs.Where(b => b.BlogCategoryId == blogCateID && b.BlogStatus == 1)
                .Include(b => b.BlogCategory)
                .Include(b => b.User)
                .OrderByDescending(b => b.BlogId)
                .Skip(itemsToSkip)
                .Take(pagging.PageSize)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();
            }
            else
            {
                blogList = await context.Blogs.Where(b => b.BlogCategoryId == blogCateID && b.BlogStatus == 1)
                .Include(b => b.BlogCategory)
                .Include(b => b.User)
                .Skip(itemsToSkip)
                .Take(pagging.PageSize)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();
            }

            return blogList;
        }
        //done
        public async Task<int> GetBlogByCateCount(int blogCateID)
        {
            int count;
            count = await context.Blogs.Where(b => b.BlogStatus == 1 && b.BlogCategoryId == blogCateID)
                .CountAsync();

            return count;
        }
        //done
        public async Task<List<BlogSimpleVM>> GetBlogByUserID(int userID, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            List<BlogSimpleVM> blogList = new();
            if (pagging.OrderBy)
            {

                blogList = await context.Blogs.Where(b => b.UserId == userID && b.BlogStatus == 1)
                    .Include(b => b.BlogCategory)
                    .Include(b => b.User)
                    .Skip(itemsToSkip)
                    .Take(pagging.PageSize)
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
                    .Take(pagging.PageSize)
                    .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                    .ToListAsync();
            }

            return blogList;
        }
        //done
        public async Task<int> GetBlogByUserIDCount(int userID)
        {
            int count;
            count = await context.Blogs.Where(b => b.UserId == userID && b.BlogStatus == 1)
                .CountAsync();

            return count;
        }
        //done
        public async Task<List<BlogSimpleVM>> GetBlogBySelf(int userID, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            List<BlogSimpleVM> blogList = new();
            if (pagging.OrderBy)
            {
                blogList = await context.Blogs.Where(b => b.UserId == userID && b.BlogStatus != 3)
                .Include(b => b.User)
                .Include(b => b.BlogCategory)
                .Skip(itemsToSkip)
                .Take(pagging.PageSize)
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
                .Take(pagging.PageSize)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();
            }

            return blogList;
        }
        //done
        public async Task<int> GetBlogByBySelfCount(int userID)
        {
            int count;
            count = await context.Blogs.Where(b => b.UserId == userID && b.BlogStatus != 3)
                .CountAsync();

            return count;
        }
        //done
        public async Task<bool> CreateBlog(CreateBlogVM blogvm, int UserID)
        {

            Blog newblog = new()
            {
                BlogCategoryId = blogvm.BlogCategoryId,
                UserId = UserID,
                BlogTitle = blogvm.BlogTitle,
                Image = blogvm.Image,
                BlogDescription = blogvm.BlogDescription,
                BlogContent = blogvm.BlogContent,
                BlogDateCreate = DateTime.Now,
                BlogStatus = 0
            };
            await context.Blogs.AddAsync(newblog);
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;

        }
        //done
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
        //done
        public async Task<bool> ConfirmBlog(Blog blog, int decision)
        {
            blog.BlogStatus = decision;
            blog.ReasonDeny = null;
            await context.SaveChangesAsync();
            if (decision == 1)
            {
                Email newEmail = new()
                {
                    To = blog.User.UserGmail,
                    Subject = "your blog has been accepted",
                    UserName = blog.User.UserName,
                    Obj = "Blog",
                    Link = "https://moby-customer.vercel.app/blog/" + blog.BlogId
                };
                await emailDAO.SendEmai(newEmail);
            }
            return true;
        }
        //done
        public async Task<bool> DenyBlog(Blog blog, String reason)
        {
            blog.BlogStatus = 2;
            blog.ReasonDeny = reason;
            await context.SaveChangesAsync();
            Email newEmail = new()
            {
                To = blog.User.UserGmail,
                Subject = "your blog has been denied",
                UserName = blog.User.UserName,
                Obj = "Blog",
                Link = "https://moby-customer.vercel.app/blog/" + blog.BlogId
            };
            await emailDAO.SendEmai(newEmail);
            return true;
        }
    }
}
