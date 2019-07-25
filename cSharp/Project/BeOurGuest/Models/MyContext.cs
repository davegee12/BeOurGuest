using BeOurGuest.Models;
using Microsoft.EntityFrameworkCore;

namespace BeOurGuest
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) {}

        public DbSet<RegUser> Users {get;set;}
        public DbSet<Character> Characters{get;set;}
        public DbSet<Like> Likes{get;set;}
        public DbSet<Restaurant> Restaurants{get;set;}

    }
}