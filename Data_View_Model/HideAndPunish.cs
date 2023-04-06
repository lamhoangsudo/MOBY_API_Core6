using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class HideAndPunish
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DefaultValue(0)]
        [Range(0,4)]
        public int Tyle { get; set; }
    }
}
