using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Repository
{
    public interface IReplyRepository
    {
        public Task<bool> CreateReply(CreateReplyVM rep, int userId);
        public Task<bool> UpdateReply(UpdateReplyVM rep, int userId);
        public Task<bool> DeleteReply(GetReplyIDVM rep, int userId);
    }
}
