using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly MOBYContext context;
        public BlogRepository(MOBYContext context)
        {
            this.context = context;
        }
        public async Task<List<BlogSimpleVM>> GetAllBlog(PaggingVM pagging)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            if (pagging.OrderBy)
            {
                return await context.Blogs
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
                return await context.Blogs
                .Include(b => b.BlogCategory)
                .Include(b => b.User)
                .Where(b => b.BlogStatus == 1)
                .Skip(itemsToSkip)
                .Take(pagging.PageSize)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();
            }
            throw new NullReferenceException();
        }
        public async Task<int> GetAllBlogCount()
        {
            return await context.Blogs
                .Where(b => b.BlogStatus == 1)
                .CountAsync();
        }
        public async Task<List<BlogSimpleVM>> SearchlBlog(PaggingVM pagging, string tittle)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            if (pagging.OrderBy)
            {
                return await context.Blogs
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
                return await context.Blogs
                .Include(b => b.BlogCategory)
                .Include(b => b.User)
                .Where(b => b.BlogStatus == 1 && b.BlogTitle.Contains(tittle))
                .Skip(itemsToSkip)
                .Take(pagging.PageSize)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();
            }
        }
        public async Task<int> GetSearchBlogCount(string tittle)
        {
            int count = await context.Blogs
                .Where(b => b.BlogStatus == 1 && b.BlogTitle
                .Contains(tittle))
                .CountAsync();
            return count;
        }
        public async Task<List<BlogBriefVM>> GetAllUncheckBlog(PaggingVM pagging, BlogStatusVM blogStatusVM)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            if (blogStatusVM.BlogStatus == null)
            {
                return await context.Blogs
                    .Include(b => b.User)
                    .Skip(itemsToSkip)
                    .Take(pagging.PageSize)
                    .Select(b => BlogBriefVM.BlogBriefToVewModel(b))
                    .ToListAsync();
            }
            else
            {
                return await context.Blogs
                    .Include(b => b.User)
                    .Where(b => b.BlogStatus == blogStatusVM.BlogStatus)
                    .Skip(itemsToSkip).Take(pagging.PageSize)
                    .Select(b => BlogBriefVM.BlogBriefToVewModel(b))
                    .ToListAsync();
            }
            throw new NullReferenceException();
        }
        public async Task<int> GetAllUncheckBlogcount(BlogStatusVM blogStatusVM)
        {
            if (blogStatusVM.BlogStatus == null)
            {
                return await context.Blogs.CountAsync();
            }
            else
            {
                return await context.Blogs.Where(b => b.BlogStatus == blogStatusVM.BlogStatus).CountAsync();
            }
            throw new InvalidDataException();
        }
        public async Task<Blog?> GetBlogByBlogID(int id)
        {
            return await context.Blogs
                .Where(b => b.BlogId == id)
                .Include(b => b.User)
                .FirstOrDefaultAsync();
        }
        public async Task<BlogVM?> GetBlogVMByBlogID(int id)
        {
            return await context.Blogs.Where(b => b.BlogId == id)
                .Include(b => b.BlogCategory)
                .Include(b => b.User)
                .Include(b => b.Comments)
                .ThenInclude(c => c.User)
                .Include(b => b.Comments)
                .ThenInclude(cmt => cmt.Replies)
                .ThenInclude(rep => rep.User)
                .Select(b => BlogVM.BlogToVewModel(b))
                .FirstOrDefaultAsync();
        }
        public async Task<Blog?> GetBlogByBlogIDAndUserId(int blogId, int userId)
        {
            return await context.Blogs
                .Where(b => b.BlogId == blogId && b.UserId == userId)
                .FirstOrDefaultAsync();
        }
        public async Task<List<BlogSimpleVM>> GetBlogByBlogCateID(int blogCateID, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            if (pagging.OrderBy)
            {
                return await context.Blogs.Where(b => b.BlogCategoryId == blogCateID && b.BlogStatus == 1)
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
                return await context.Blogs.Where(b => b.BlogCategoryId == blogCateID && b.BlogStatus == 1)
                .Include(b => b.BlogCategory)
                .Include(b => b.User)
                .Skip(itemsToSkip)
                .Take(pagging.PageSize)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();
            }
        }
        public async Task<int> GetBlogByCateCount(int blogCateID)
        {
            return await context.Blogs
                .Where(b => b.BlogStatus == 1 && b.BlogCategoryId == blogCateID)
                .CountAsync();
        }
        public async Task<List<BlogSimpleVM>> GetBlogByUserID(int userID, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            if (pagging.OrderBy)
            {
                return await context.Blogs.Where(b => b.UserId == userID && b.BlogStatus == 1)
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
                return await context.Blogs.Where(b => b.UserId == userID && b.BlogStatus == 1)
                    .Include(b => b.BlogCategory)
                    .Include(b => b.User)
                    .Skip(itemsToSkip)
                    .Take(pagging.PageSize)
                    .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                    .ToListAsync();
            }
        }
        public async Task<int> GetBlogByUserIDCount(int userID)
        {
            return await context.Blogs
                .Where(b => b.UserId == userID && b.BlogStatus == 1)
                .CountAsync();
        }
        public async Task<List<BlogSimpleVM>> GetBlogBySelf(int userID, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            if (pagging.OrderBy)
            {
                return await context.Blogs.Where(b => b.UserId == userID && b.BlogStatus != 3)
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
                return await context.Blogs.Where(b => b.UserId == userID && b.BlogStatus != 3)
                .Include(b => b.User)
                .Include(b => b.BlogCategory)
                .Skip(itemsToSkip)
                .Take(pagging.PageSize)
                .Select(b => BlogSimpleVM.BlogSimpleToVewModel(b))
                .ToListAsync();
            }
        }
        public async Task<int> GetBlogByBySelfCount(int userID)
        {
            return await context.Blogs
                .Where(b => b.UserId == userID && b.BlogStatus != 3)
                .CountAsync();
        }
        public async Task<int> CreateBlog(CreateBlogVM blogvm, int UserID)
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
            return await context.SaveChangesAsync();
        }
        public async Task<int> UpdateBlog(Blog blog, UpdateBlogVM blogvm)
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
                return await context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<int> ConfirmBlog(Blog blog, int decision)
        {
            blog.BlogStatus = decision;
            blog.ReasonDeny = null;
            return await context.SaveChangesAsync();
        }
        public async Task<int> DenyBlog(Blog blog, string reason)
        {
            blog.BlogStatus = 2;
            blog.ReasonDeny = reason;
            return await context.SaveChangesAsync();
        }
    }
}
