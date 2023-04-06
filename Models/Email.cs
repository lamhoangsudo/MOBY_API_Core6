using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Models
{
    public class Email
    {
        [Required]
        public string To { get; set; } = string.Empty;
        [Required]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public string Body { get; set; } = string.Empty;
    }
}
