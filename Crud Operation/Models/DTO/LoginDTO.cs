using System.ComponentModel.DataAnnotations;

namespace Crud_Operation.Models.DTO
{
    public class LoginDTO
    {
        [Display(Name = "E-mail or Username")]
        [Required]
        public string EmailOrUsername { get; set; }
        [Required]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
