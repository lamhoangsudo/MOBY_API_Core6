﻿using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class ReplyVM
    {
        public int ReplyId { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string ReplyContent { get; set; } = null!;
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? Status { get; set; }
        public string? ReasonHiden { get; set; }
        public UserVM? UserVM { get; set; }

        public static ReplyVM ReplyToVewModel(Reply rep)
        {
            ReplyVM replyVM = new()
            {
                ReplyId = rep.ReplyId,
                CommentId = rep.CommentId,
                UserId = rep.UserId,
                ReplyContent = rep.ReplyContent,
                DateCreate = rep.DateCreate,
                DateUpdate = rep.DateUpdate,
                Status = rep.Status,
                ReasonHiden = rep.ReasonHiden,
            };
            var user = rep.User;
            replyVM.UserVM = UserVM.UserAccountToVewModel(user);
            return replyVM;
        }
    }
}
