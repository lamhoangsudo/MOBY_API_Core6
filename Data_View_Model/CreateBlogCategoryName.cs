using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateBlogCategoryName
    {
        [DefaultValue("String blogCateName")]
        public string? BlogCategoryName { get; set; }
    }
}
