using System.ComponentModel.DataAnnotations;

namespace CdplATS.Entity.Models
{
    public class LoginEntity
    {
        [Required(ErrorMessage = "UserCode is required")]
        //[EmailAddress(ErrorMessage = "Invalid Username")]
        public int? UserCode { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
