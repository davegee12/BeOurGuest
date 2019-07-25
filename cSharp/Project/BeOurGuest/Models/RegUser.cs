using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace BeOurGuest.Models
{
    public class RegUser
    {
        [Key]
        public int RegUserId{get;set;}
        // First Name
        [Required(ErrorMessage="First Name is required")]
        [MinLength(2, ErrorMessage="First name must be longer than two characters")]
        public string FName{get;set;}

        //Last Name
        [Required(ErrorMessage="Last Name is required")]
        [MinLength(2, ErrorMessage="Last name must be longer than two characters")]
        public string LName{get;set;}

        // Email
        [Required(ErrorMessage="Email is required")]
        [EmailAddress(ErrorMessage="Must be valid email format")]
        public string Email{get;set;}

        // Password
        [Required(ErrorMessage="Password is required")]
        [MinLength(8, ErrorMessage="Password must be eight or more characters")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Password must contain a minimum of eight characters, at least one letter, one number and one special character")]
        [DataType(DataType.Password)]
        public string Password{get;set;}

        // Confirm
        [NotMapped]
        [Required(ErrorMessage="Please confirm your password")]
        [MinLength(8)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string Confirm{get;set;}

        // Image
        public string Image{get;set;}

        // Location
        [Required(ErrorMessage="Location is required")]
        public string Location{get;set;}

        // Price
        [Required(ErrorMessage="Price is required")]
        public int Price{get;set;}

        // Foreign Key to Characters (One to Many)

        public Character TopCharacter {get;set;}
        public string SecondCharacter {get;set;}
        public string ThirdCharacter {get;set;}

        // Foreign Key to Orders (Many to Many)

        public List<Like> RestaurantsLiked {get;set;}

        public DateTime CreatedAt{get;set;} = DateTime.Now;
        public DateTime UpdatedAt{get;set;} = DateTime.Now;

    }
}
