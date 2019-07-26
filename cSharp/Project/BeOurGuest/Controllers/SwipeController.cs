using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeOurGuest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace BeOurGuest.Controllers
{
    public class SwipeController : Controller
    {
        private MyContext dbContext;

        // here we can "inject" our context service into the constructor
        public SwipeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("swipe")]
        public ViewResult Swipe()
        {
            // if (HttpContext.Session.GetInt32("Id") == null)
            // {
            //     ModelState.AddModelError("Email", "Please log in or register!");
            //     return View("Index");
            // }
            var thisUser = dbContext.Users
            .FirstOrDefault(user => user.RegUserId == HttpContext.Session.GetInt32("LoggedInId"));

            return View("Swipe");
        }

        [HttpGet("edit")]
        public ViewResult ShowEdit()
        {
            // if (HttpContext.Session.GetInt32("Id") == null)
            // {
            //     ModelState.AddModelError("Email", "Please log in or register!");
            //     return View("Index");
            // }
            RegUser thisUser = dbContext.Users
            .FirstOrDefault(user => user.RegUserId == HttpContext.Session.GetInt32("LoggedInId"));
            ViewBag.User = thisUser;
            Console.WriteLine(ViewBag.User.FName);
            return View("Edit");
        }

        [HttpPost("update")]
        public IActionResult EditUser(RegUser edittedInfo)
        {
            Console.WriteLine("HELLLOOOO");
            Console.WriteLine(edittedInfo.FName);
            Console.WriteLine("&&&&&&&&&&&&&&&&&&&&");
            RegUser editUser = dbContext.Users.FirstOrDefault(user => user.RegUserId == edittedInfo.RegUserId);
            Console.WriteLine(editUser.FName);
            editUser.FName = edittedInfo.FName;
            editUser.LName = edittedInfo.LName;
            editUser.Location = edittedInfo.Location;
            editUser.Price = edittedInfo.Price;
            editUser.Email = edittedInfo.Email;
            editUser.UpdatedAt = DateTime.Now;
            dbContext.SaveChanges();
            return Redirect("/swipe");
            
        }
    }
}