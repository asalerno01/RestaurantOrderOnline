using System.ComponentModel.DataAnnotations;

namespace SalernoServer.Models.Authentication
{
    public class AccountLogin
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public AccountLogin() { }
    }
}