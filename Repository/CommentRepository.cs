using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MOBYContext context;

        public CommentRepository(MOBYContext context)
        {
            this.context = context;
        }

        public List<Comment> GetAllComment()
        {
            List<Comment> ListComment = context.Comments.Include(c => c.Replies).ToList();
            return ListComment;
        }

        public List<Comment> GetCommentByBlogID(int id)
        {
            List<Comment> ListComment = context.Comments.Where(cmt => cmt.BlogId == id).ToList();


            return ListComment;
        }

        public List<Comment> GetCommentByItemID(int id)
        {
            List<Comment> ListComment = context.Comments.Where(cmt => cmt.ItemId == id).ToList();
            return ListComment;
        }

        public bool CreateComment(CreateCommentVM cmt, int userId)
        {
            Comment newCmt = new Comment();
            newCmt.ItemId = cmt.ItemId;
            newCmt.BlogId = cmt.BlogId;
            newCmt.UserId = userId;
            newCmt.CommentContent = cmt.CommentContent;
            newCmt.DateCreate = DateTime.Now;

            context.Add(newCmt);
            context.SaveChanges();
            return true;
        }

        public bool UpdateComment(UpdateCommentVM cmt, int userId)
        {
            Comment currentcmt = context.Comments.Where(cmt => cmt.CommentId == cmt.CommentId && cmt.UserId == userId).FirstOrDefault();

            if (currentcmt != null)
            {
                currentcmt.CommentContent = cmt.CommentContent;
                currentcmt.DateUpdate = DateTime.Now;

                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteComment(GetCommentIDVM cmt, int userId)
        {
            Comment currentcmt = context.Comments.Where(cmt => cmt.CommentId == cmt.CommentId && cmt.UserId == userId).FirstOrDefault();

            if (currentcmt != null)
            {
                context.Remove(currentcmt);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
