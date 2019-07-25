using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace BeOurGuest.Models
{
    public class Like
    {
        [Key]
        public int LikeId{get;set;}

        public int RegUserId{get;set;}
        public int CharacterId{get;set;}
        public int RestaurantId{get;set;}

        // Image
        [Required(ErrorMessage="Image is required")]
        public string Image{get;set;}

        public DateTime CreatedAt{get;set;} = DateTime.Now;
        public DateTime UpdateAt{get;set;} = DateTime.Now;

        // Navigation

        public RegUser Liker{get;set;}
        public Like RestaurantLiked{get;set;}
    }
}