using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class ReplyRepository : IReplyRepository
    {
        private readonly MOBYContext context;

        public ReplyRepository(MOBYContext context)
        {
            this.context = context;
        }
        public async Task<bool> CreateReply(CreateReplyVM rep, int userId)
        {
            Reply newRep = new Reply();
            newRep.CommentId = rep.CommentId;
            newRep.UserId = userId;
            newRep.ReplyContent = rep.ReplyContent;
            newRep.DateCreate = DateTime.Now;

            await context.AddAsync(newRep);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateReply(UpdateReplyVM rep, int userId)
        {
            Reply? currentRep = await context.Replies.Where(r => r.ReplyId == rep.ReplyId && r.UserId == userId).FirstOrDefaultAsync();

            if (currentRep != null)
            {
                currentRep.ReplyContent = rep.ReplyContent;
                currentRep.DateUpdate = DateTime.Now;

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteReply(GetReplyIDVM rep, int userId)
        {
            Reply? currentRep = await context.Replies.Where(r => r.ReplyId == rep.ReplyId && r.UserId == userId).FirstOrDefaultAsync();

            if (currentRep != null)
            {
                context.Remove(currentRep);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
