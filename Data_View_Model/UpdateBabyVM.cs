using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UpdateBabyVM
    {
        [Required]
        public int Idbaby { get; set; }
        [ReadOnly(true)]
        public int UserID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Sex { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
    }
}
