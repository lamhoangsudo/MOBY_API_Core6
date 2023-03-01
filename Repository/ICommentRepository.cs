using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface ICommentRepository
    {
        public List<Comment> GetAllComment();
        public List<Comment> GetCommentByBlogID(int id);
        public List<Comment> GetCommentByItemID(int id);
        public bool CreateComment(CreateCommentVM cmt, int userId);
        public bool UpdateComment(UpdateCommentVM cmt, int userId);
        public bool DeleteComment(GetCommentIDVM cmt, int userId);
    }
}
