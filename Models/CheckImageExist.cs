using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Models
{
    public class CheckImageExist
    {
        [Key]
        public int ImageId { get; set; }
    }
}
