using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class GetUserObj
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [DefaultValue(0)]
        [Range(0, 4)]
        public int Type { get; set; }
    }
}
