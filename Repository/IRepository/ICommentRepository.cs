using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Repository.IRepository
{
    public interface ICommentRepository
    {
        public Task<List<CommentVM>> GetAllComment();
        public Task<CommentVM?> GetCommentByCommentID(int id);
        public Task<List<CommentVM>> GetCommentByBlogID(int id);
        public Task<List<CommentVM>> GetCommentByItemID(int id);
        public Task<bool> CreateComment(CreateCommentVM cmt, int userId);
        public Task<bool> UpdateComment(UpdateCommentVM cmt, int userId);
        public Task<bool> DeleteComment(GetCommentIDVM cmt, int userId);
    }
}
