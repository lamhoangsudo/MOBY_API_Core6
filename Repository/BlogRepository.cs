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

        public async Task<List<BlogVM>> getAllBlog(PaggingVM pagging)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<BlogVM> blogList = await context.Blogs.Where(b => b.BlogStatus == 1)
                .OrderByDescending(b => b.BlogId)
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .Select(b => BlogVM.BlogToVewModel(b))
                .ToListAsync();

            return blogList;
        }
        public async Task<int> getAllBlogCount()
        {
            int count;
            count = await context.Blogs.Where(b => b.BlogStatus == 1)
                .CountAsync();

            return count;
        }

        public async Task<List<BlogVM>> getAllUncheckBlog(PaggingVM pagging)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<BlogVM> blogList = await context.Blogs.Where(b => b.BlogStatus == 0)
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .Select(b => BlogVM.BlogToVewModel(b))
                .ToListAsync();

            return blogList;
        }
        public async Task<Blog?> getBlogByBlogID(int id)
        {

            Blog? blogFound = await context.Blogs.Where(b => b.BlogId == id)
                .Include(b => b.Comments)
                .ThenInclude(cmt => cmt.Replies)
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

        public async Task<List<BlogVM>> getBlogByBlogCateID(int blogCateID, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<BlogVM> blogList = await context.Blogs
                .Where(b => b.BlogCategoryId == blogCateID && b.BlogStatus == 1)
                .OrderByDescending(b => b.BlogId)
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .Select(b => BlogVM.BlogToVewModel(b))
                .ToListAsync();

            return blogList;
        }
        public async Task<int> getAllBlogCount(int blogCateID)
        {
            int count;
            count = await context.Blogs.Where(b => b.BlogStatus == 1 && b.BlogCategoryId == blogCateID)
                .CountAsync();

            return count;
        }
        public async Task<List<BlogVM>> getBlogByUserID(int userID, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<BlogVM> blogList = new List<BlogVM>();
            blogList = await context.Blogs
                .Where(b => b.UserId == userID && b.BlogStatus == 1)
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .Select(b => BlogVM.BlogToVewModel(b))
                .ToListAsync();

            return blogList;
        }
        public async Task<int> getBlogByUserIDCount(int userID)
        {
            int count;
            count = await context.Blogs.Where(b => b.UserId == userID && b.BlogStatus == 1)
                .CountAsync();

            return count;
        }

        public async Task<List<BlogVM>> getBlogBySelf(int userID, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<BlogVM> blogList = await context.Blogs
                .Where(b => b.UserId == userID && b.BlogStatus != 3)
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .Select(b => BlogVM.BlogToVewModel(b))
                .ToListAsync();

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
                }
                if (await context.SaveChangesAsync() != 0)
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
        public async Task<bool> ConfirmBlog(Blog blog, int decision)
        {
            blog.BlogStatus = decision;
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DenyBlog(Blog blog, int decision, String reason)
        {
            blog.BlogStatus = decision;
            blog.ReasonDeny = reason;
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }

            return false;
        }

    }
}
