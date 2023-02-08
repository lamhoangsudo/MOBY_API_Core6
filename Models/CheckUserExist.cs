using System.ComponentModel.DataAnnotations;

namespace Item.Models
{
    public class CheckUserExist
    {
        [Key]
        public int UserId { get; set; }
    }
}
