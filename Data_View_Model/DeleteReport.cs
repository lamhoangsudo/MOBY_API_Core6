using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class DeleteReport
    {
        [Required]
        public int reportID { get; set; }
        [DefaultValue(3)]
        [ReadOnly(true)]
        public int isDelete { get; set; }
    }
}
