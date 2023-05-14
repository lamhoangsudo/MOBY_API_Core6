using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class CommentService : ICommentService
    {
        private readonly MOBYContext context;
        private readonly ICommentRepository commentRepository;
        public CommentService(MOBYContext context, ICommentRepository commentRepository)
        {
            this.context = context;
            this.commentRepository = commentRepository;
        }

        public async Task<List<CommentVM>> GetAllComment()
        {
            List<CommentVM> ListComment = await commentRepository.GetAllComment();
            return ListComment;
        }
        public async Task<CommentVM?> GetCommentByCommentID(int id)
        {
            CommentVM? Comment = await commentRepository.GetCommentByCommentID(id);
            return Comment;
        }

        public async Task<List<CommentVM>> GetCommentByBlogID(int id)
        {
            List<CommentVM> ListComment = await commentRepository.GetCommentByBlogID(id);
            return ListComment;
        }

        public async Task<List<CommentVM>> GetCommentByItemID(int id)
        {
            List<CommentVM> ListComment = await commentRepository.GetCommentByItemID(id);
            return ListComment;
        }

        public async Task<bool> CreateComment(CreateCommentVM cmt, int userId)
        {
            int check = await commentRepository.CreateComment(cmt, userId);
            if (check != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateComment(UpdateCommentVM comment, int userId)
        {
            int check = await commentRepository.UpdateComment(comment, userId);
            if (check != 0)
            {
                return true;
            }
            return true;
        }

        public async Task<bool> DeleteComment(GetCommentIDVM cmt, int userId)
        {
            int check = await commentRepository.DeleteComment(cmt, userId);
            if (check != 0)
            {
                return true;
            }
            return false;
        }
    }
}
