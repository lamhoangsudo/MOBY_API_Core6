using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class ReplyService : IReplyService
    {
        private readonly MOBYContext context;

        public ReplyService(MOBYContext context)
        {
            this.context = context;
        }

        public async Task<ReplyVM?> GetReplyByReplyID(int id)
        {
            ReplyVM? foundRewply = await context.Replies.Where(r => r.ReplyId == id)
                .Include(r => r.User)
                .Select(r => ReplyVM.ReplyToVewModel(r))
                .FirstOrDefaultAsync();
            return foundRewply;
        }

        public async Task<bool> CreateReply(CreateReplyVM rep, int userId)
        {
            Reply newRep = new Reply();
            newRep.CommentId = rep.CommentId;
            newRep.UserId = userId;
            newRep.ReplyContent = rep.ReplyContent;
            newRep.Status = true;
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
