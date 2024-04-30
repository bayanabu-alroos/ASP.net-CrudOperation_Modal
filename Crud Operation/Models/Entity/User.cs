using System.ComponentModel.DataAnnotations;

namespace Crud_Operation.Models.Entity
{
    public class User : Shared
    {
        [Display(Name = "E-mail")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsLoggedIn { get; set; }

    }
}
