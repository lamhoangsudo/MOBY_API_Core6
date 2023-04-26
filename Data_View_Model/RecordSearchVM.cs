using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class RecordSearchVM
    {
        [ReadOnly(true)]
        public int UserId { get; set; }
        [DefaultValue(null)]
        public int? CategoryId { get; set; }
        [DefaultValue(null)]
        public int? SubCategoryId { get; set; }
        [DefaultValue(null)]
        public string? TitleName { get; set; }
    }
}
