using System.ComponentModel.DataAnnotations;

namespace SecuritySample.Api.Models.Forms
{
    public class RegisterForm
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Passwd { get; set; }
    }
}
