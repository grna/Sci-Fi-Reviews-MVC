using System;
using System.ComponentModel.DataAnnotations;

namespace SciFiReviews.Models.ViewModels
{
    public class SignUpViewModel
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string MiddleName { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required, MaxLength(50)]
        public string Password { get; set; }

        [Required, MaxLength(50)]
        [Compare("Password")]
        public string RepeatPassword { get; set; }

        [Required, MaxLength(50)]
        public string EmailAddress { get; set; }
    }
}
