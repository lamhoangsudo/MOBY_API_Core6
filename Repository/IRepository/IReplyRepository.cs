using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Repository.IRepository
{
    public interface IReplyRepository
    {
        public Task<ReplyVM?> GetReplyByReplyID(int id);
        public Task<int> CreateReply(CreateReplyVM rep, int userId);
        public Task<int> UpdateReply(UpdateReplyVM rep, int userId);
        public Task<int> DeleteReply(GetReplyIDVM rep, int userId);
    }
}
