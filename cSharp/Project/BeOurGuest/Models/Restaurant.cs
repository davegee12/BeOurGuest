using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace BeOurGuest.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId{get;set;}

        // Name
        [Required(ErrorMessage="Name is required")]
        [MinLength(2, ErrorMessage="Name must be longer than two characters")]
        public string Name{get;set;}

        // Address
        [Required(ErrorMessage="Address is required")]
        [MinLength(2, ErrorMessage="Address must be longer than two characters")]
        public string Address{get;set;}

        public int APIId{get;set;}
        public int Rating{get;set;}
        public DateTime CreatedAt{get;set;} = DateTime.Now;
        public DateTime UpdateAt{get;set;} = DateTime.Now;

        // Navigation

        public RegUser Guest{get;set;}
        public Like RestaurantsLiked{get;set;}
    }
}