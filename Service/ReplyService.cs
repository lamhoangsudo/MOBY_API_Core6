using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class ReplyService : IReplyService
    {
        private readonly IReplyRepository replyRepository;
        public ReplyService(IReplyRepository replyRepository)
        {
            this.replyRepository = replyRepository;
        }

        public async Task<ReplyVM?> GetReplyByReplyID(int id)
        {
            ReplyVM? foundRewply = await replyRepository.GetReplyByReplyID(id);
            return foundRewply;
        }

        public async Task<bool> CreateReply(CreateReplyVM rep, int userId)
        {
            int check = await replyRepository.CreateReply(rep, userId);
            if (check == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateReply(UpdateReplyVM rep, int userId)
        {
            int check = await replyRepository.UpdateReply(rep, userId); 
            if (check == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteReply(GetReplyIDVM rep, int userId)
        {
            int check = await replyRepository.DeleteReply(rep, userId); 
            if (check == 0)
            {
                return false;
            }
            return true;
        }
    }
}
