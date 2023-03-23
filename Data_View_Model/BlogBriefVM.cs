using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class BlogBriefVM
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; } = null!;

        public string? Image { get; set; }
        public string? BlogDescription { get; set; }
        public DateTime BlogDateCreate { get; set; }
        public int? BlogStatus { get; set; }
        public UserVM? UserVM { get; set; }

        public static BlogBriefVM BlogBriefToVewModel(Blog blog)
        {

            var blogView = new BlogBriefVM
            {
                BlogId = blog.BlogId,

                BlogTitle = blog.BlogTitle,
                Image = blog.Image,
                BlogDescription = blog.BlogDescription,

                BlogDateCreate = blog.BlogDateCreate,

                BlogStatus = blog.BlogStatus
            };
            var user = blog.User;
            blogView.UserVM = UserVM.UserAccountToVewModel(user);

            return blogView;
        }
    }
}
