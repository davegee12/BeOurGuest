using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace BeOurGuest.Models
{
    public class Character
    {
        [Key]
        public int CharacterId{get;set;}

        // Name
        [Required(ErrorMessage="Name is required")]
        [MinLength(2, ErrorMessage="Name must be longer than two characters")]
        public string Name{get;set;}

        // Cuisine
        [Required(ErrorMessage="Cuisine is required")]
        public string Cuisine{get;set;}

        // One to Many
        public List<RegUser> UsersWithThisCharacter{get;set;}
    }
}