using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class BlogVM
    {
        public int BlogId { get; set; }
        public int BlogCategoryId { get; set; }
        public int UserId { get; set; }
        public string BlogTitle { get; set; } = null!;
        public string? BlogDescription { get; set; }
        public string BlogContent { get; set; } = null!;
        public DateTime BlogDateCreate { get; set; }
        public DateTime? BlogDateUpdate { get; set; }
        public int? BlogStatus { get; set; }

        public List<CommentVM>? ListComment { get; set; }
        public static BlogVM BlogToVewModel(Blog blog)
        {

            var blogView = new BlogVM
            {
                BlogId = blog.BlogId,
                BlogCategoryId = blog.BlogCategoryId,
                UserId = blog.UserId,
                BlogTitle = blog.BlogTitle,
                BlogDescription = blog.BlogDescription,
                BlogContent = blog.BlogContent,
                BlogDateCreate = blog.BlogDateCreate,
                BlogDateUpdate = blog.BlogDateUpdate,
                BlogStatus = blog.BlogStatus
            };
            var ListComment = blog.Comments.Select(b => CommentVM.CommentToVewModel(b)).ToList();
            blogView.ListComment = ListComment;
            return blogView;
        }
    }
}
