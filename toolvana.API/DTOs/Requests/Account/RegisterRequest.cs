using System.ComponentModel.DataAnnotations;
using toolvana.API.Models;
using toolvana.API.Validations.GenderValidator;

namespace toolvana.API.DTOs.Requests.Account
{
    public class RegisterRequest
    {

        [MinLength(3)]
        public required string FirstName { get; set; }

        [MinLength(3)]
        public required string LastName { get; set; }

        [MinLength(6)]
        public required string UserName { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        public required string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]

        public required string ConfirmPassword { get; set; }

        public ApplicationGender Gender { get; set; }

        [UserAgeValidator(18, ErrorMessage = "You must be at least 18 years old to register.")]
        public DateTime DateOfBirth { get; set; }

    }
}
