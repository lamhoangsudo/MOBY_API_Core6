using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateBlogCategoryName
    {
        [Required]
        [DefaultValue("String blogCateName")]
        public string BlogCategoryName { get; set; }

        public CreateBlogCategoryName(string blogCategoryName)
        {
            BlogCategoryName = blogCategoryName;
        }
    }
}
