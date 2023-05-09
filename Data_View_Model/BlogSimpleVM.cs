using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class BlogSimpleVM
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; } = null!;
        public string? Image { get; set; }
        public string? BlogDescription { get; set; }
        public string? ReasonDeny { get; set; }
        public DateTime BlogDateCreate { get; set; }
        public DateTime? BlogDateUpdate { get; set; }
        public int? BlogStatus { get; set; }
        public UserVM? UserVM { get; set; }
        public BlogCategoryVMForBlogVM? BlogCategory { get; set; }

        public static BlogSimpleVM BlogSimpleToVewModel(Blog blog)
        {

            var blogView = new BlogSimpleVM
            {
                BlogId = blog.BlogId,

                BlogTitle = blog.BlogTitle,
                Image = blog.Image,
                BlogDescription = blog.BlogDescription,
                ReasonDeny = blog.ReasonDeny,
                BlogDateCreate = blog.BlogDateCreate,
                BlogDateUpdate = blog.BlogDateUpdate,
                BlogStatus = blog.BlogStatus
            };
            var blogcate = blog.BlogCategory;
            blogView.BlogCategory = BlogCategoryVMForBlogVM.BlogCategoryVMForBlogVMToVewModel(blogcate);
            var user = blog.User;
            blogView.UserVM = UserVM.UserAccountToVewModel(user);

            return blogView;
        }
    }
}
