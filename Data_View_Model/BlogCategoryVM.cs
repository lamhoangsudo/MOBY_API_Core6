using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class BlogCategoryVM
    {
        public int BlogCategoryId { get; set; }
        public string BlogCategoryName { get; set; } = null!;
        public List<BlogVM>? blogList { get; set; }

        public static BlogCategoryVM BlogCategoryVMToVewModel(BlogCategory blogcate)
        {
            var blogcateView = new BlogCategoryVM
            {
                BlogCategoryId = blogcate.BlogCategoryId,
                BlogCategoryName = blogcate.BlogCategoryName,
            };
            var ListBlog = blogcate.Blogs.Select(bc => BlogVM.BlogToVewModel(bc)).ToList();
            blogcateView.blogList = ListBlog;
            return blogcateView;
        }
    }
}
