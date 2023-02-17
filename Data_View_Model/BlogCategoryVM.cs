using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class BlogCategoryVM
    {
        public int BlogCategoryId { get; set; }
        public string BlogCategoryName { get; set; } = null!;

        public static BlogCategoryVM BlogCategoryVMToVewModel(BlogCategory blogcate)
        {
            return new BlogCategoryVM
            {
                BlogCategoryId = blogcate.BlogCategoryId,
                BlogCategoryName = blogcate.BlogCategoryName,

            };
        }
    }
}
