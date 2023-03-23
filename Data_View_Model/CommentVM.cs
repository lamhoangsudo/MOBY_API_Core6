using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CommentVM
    {
        public int CommentId { get; set; }
        public int? ItemId { get; set; }
        public int? BlogId { get; set; }
        public int UserId { get; set; }
        public string CommentContent { get; set; } = null!;
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool EditStatus { get; set; }
        public bool CommentStatus { get; set; }
        public UserVM? UserVM { get; set; }
        public List<ReplyVM>? ListReply { get; set; }


        public static CommentVM CommentToVewModel(Comment cmt)
        {
            var commendVM = new CommentVM
            {
                CommentId = cmt.CommentId,
                ItemId = cmt.ItemId,
                BlogId = cmt.BlogId,
                UserId = cmt.UserId,
                CommentContent = cmt.CommentContent,
                DateCreate = cmt.DateCreate,
                DateUpdate = cmt.DateUpdate,
            };
            var user = cmt.User;
            commendVM.UserVM = UserVM.UserAccountToVewModel(user);
            var listReplies = cmt.Replies.Select(rep => ReplyVM.ReplyToVewModel(rep)).ToList();
            commendVM.ListReply = listReplies;

            return commendVM;
        }
    }
}
