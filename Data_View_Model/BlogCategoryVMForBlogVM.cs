using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class BlogCategoryVMForBlogVM
    {
        public int BlogCategoryId { get; set; }
        public string BlogCategoryName { get; set; } = null!;
        public static BlogCategoryVMForBlogVM BlogCategoryVMForBlogVMToVewModel(BlogCategory blogcate)
        {
            var blogcateView = new BlogCategoryVMForBlogVM
            {
                BlogCategoryId = blogcate.BlogCategoryId,
                BlogCategoryName = blogcate.BlogCategoryName,
            };

            return blogcateView;
        }
    }
}
