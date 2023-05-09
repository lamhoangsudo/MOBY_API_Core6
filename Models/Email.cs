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
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Obj { get; set; } = string.Empty;
        [Required]
        public string Link { get; set; } = string.Empty;

    }
}
