using System.ComponentModel.DataAnnotations;

namespace MvcMany.Models.AccountModel
{
    public class RegisterModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public string AccessToken { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
    }
}
