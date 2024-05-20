using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.DTO
{
    public class RegisterDto
    {
        public int? Id { get; set; }
       // [Required]
        public string? FirstName { get; set; }
       // [Required]
        public string? LastName { get; set; }
        //[Required]
        //[DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        //[Required]
        //[DataType(DataType.Password)]
        //[StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
        public string? Password { get; set; }


        //[Compare("Password", ErrorMessage = "Passwords don't match.")]
        //[Display(Name = "Confirm Password")]
        //[DataType(DataType.Password)]
        //[StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
        public string? ConfirmPassword { get; set; }
    }
}
