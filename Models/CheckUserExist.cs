using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Models
{
    public class CheckUserExist
    {
        [Key]
        public int UserId { get; set; }
    }
}
