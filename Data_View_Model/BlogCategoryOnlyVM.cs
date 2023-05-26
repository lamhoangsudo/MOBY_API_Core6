using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class BlogCategoryOnlyVM
    {
        public int BlogCategoryId { get; set; }
        public string BlogCategoryName { get; set; } = null!;
        public bool? Status { get; set; }

        public static BlogCategoryOnlyVM BlogCategoryToVewModel(BlogCategory blogcate)
        {
            var blogcateView = new BlogCategoryOnlyVM
            {
                BlogCategoryId = blogcate.BlogCategoryId,
                BlogCategoryName = blogcate.BlogCategoryName,
                Status = blogcate.Status,
            };

            return blogcateView;
        }
    }
}
