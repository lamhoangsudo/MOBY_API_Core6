using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class BlogCategoryVM
    {
        public int BlogCategoryId { get; set; }
        public string BlogCategoryName { get; set; } = null!;
        public string? Status { get; set; }
        public List<BlogBriefVM>? BlogList { get; set; }

        public static BlogCategoryVM BlogCategoryVMToVewModel(BlogCategory blogcate)
        {
            var blogcateView = new BlogCategoryVM
            {
                BlogCategoryId = blogcate.BlogCategoryId,
                BlogCategoryName = blogcate.BlogCategoryName,
                Status = blogcate.Status,
            };
            var ListBlog = blogcate.Blogs.Select(bc => BlogBriefVM.BlogBriefToVewModel(bc)).ToList();
            blogcateView.BlogList = ListBlog;
            return blogcateView;
        }
    }
}
