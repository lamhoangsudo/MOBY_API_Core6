using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Repository
{
    public class ReplyRepository : IReplyRepository
    {
        private readonly MOBYContext context;

        public ReplyRepository(MOBYContext context)
        {
            this.context = context;
        }
        public async Task<ReplyVM?> GetReplyByReplyID(int id)
        {
            return await context.Replies.Where(r => r.ReplyId == id)
                .Include(r => r.User)
                .Select(r => ReplyVM.ReplyToVewModel(r))
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateReply(CreateReplyVM rep, int userId)
        {
            Reply newRep = new()
            {
                CommentId = rep.CommentId,
                UserId = userId,
                ReplyContent = rep.ReplyContent,
                Status = true,
                DateCreate = DateTime.Now
            };
            await context.AddAsync(newRep);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateReply(UpdateReplyVM rep, int userId)
        {
            Reply? currentRep = await context.Replies.Where(r => r.ReplyId == rep.ReplyId && r.UserId == userId).FirstOrDefaultAsync();
            if (currentRep != null)
            {
                currentRep.ReplyContent = rep.ReplyContent;
                currentRep.DateUpdate = DateTime.Now;
                return await context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteReply(GetReplyIDVM rep, int userId)
        {
            Reply? currentRep = await context.Replies.Where(r => r.ReplyId == rep.ReplyId && r.UserId == userId).FirstOrDefaultAsync();

            if (currentRep != null)
            {
                context.Remove(currentRep);
                return await context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
