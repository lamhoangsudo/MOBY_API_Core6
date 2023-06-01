using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MOBYContext context;

        public CommentRepository(MOBYContext context)
        {
            this.context = context;
        }
        public async Task<List<CommentVM>> GetAllComment()
        {
            return await context.Comments
                .Include(c => c.User)
                .Include(c => c.Replies)
                .ThenInclude(rep => rep.User)
                .Select(c => CommentVM.CommentToVewModel(c))
                .ToListAsync();
        }
        public async Task<CommentVM?> GetCommentByCommentID(int id)
        {
            return await context.Comments.Where(cmt => cmt.CommentId == id)
                .Include(c => c.User)
                .Select(c => CommentVM.CommentOnlyToVewModel(c))
                .FirstOrDefaultAsync();
        }
        public async Task<List<CommentVM>> GetCommentByBlogID(int id)
        {
            return await context.Comments.Where(cmt => cmt.BlogId == id)
                .Include(c => c.User)
                .Include(c => c.Replies)
                .ThenInclude(rep => rep.User)
                .Select(c => CommentVM.CommentToVewModel(c))
                .ToListAsync();
        }
        public async Task<List<CommentVM>> GetCommentByItemID(int id)
        {
            return await context.Comments.Where(cmt => cmt.ItemId == id)
                .Include(c => c.User)
                .Include(c => c.Replies)
                .ThenInclude(rep => rep.User)
                .Select(c => CommentVM.CommentToVewModel(c))
                .ToListAsync();
        }
        public async Task<int> CreateComment(CreateCommentVM cmt, int userId)
        {
            Comment newCmt = new()
            {
                ItemId = cmt.ItemId,
                BlogId = cmt.BlogId,
                UserId = userId,
                CommentContent = cmt.CommentContent,
                Status = true,
                DateCreate = DateTime.Now
            };
            await context.AddAsync(newCmt);
            return await context.SaveChangesAsync();
        }
        public async Task<int> UpdateComment(UpdateCommentVM comment, int userId)
        {
            Comment? currentcmt = await context.Comments.Where(c => c.CommentId == comment.CommentId && c.UserId == userId).FirstOrDefaultAsync();
            if (currentcmt != null)
            {
                currentcmt.CommentContent = comment.CommentContent;
                currentcmt.DateUpdate = DateTime.Now;
                currentcmt.ReasonHiden = null;
                return await context.SaveChangesAsync();
            }
            return 0;
        }
        public async Task<int> DeleteComment(GetCommentIDVM cmt, int userId)
        {
            Comment? currentcmt = await context.Comments.Where(c => c.CommentId == cmt.CommentId && c.UserId == userId)
                .Include(c => c.Replies).FirstOrDefaultAsync();
            if (currentcmt != null)
            {
                if (currentcmt.Replies != null && currentcmt.Replies.Count != 0)
                {
                    List<Reply> listRep = currentcmt.Replies.ToList();
                    foreach (Reply rep in listRep)
                    {
                        context.Replies.Remove(rep);
                    }
                }
                context.Comments.Remove(currentcmt);
                return await context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
