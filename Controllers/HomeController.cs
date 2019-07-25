using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeOurGuest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace BeOurGuest.Controllers
{
    public class YelpAPIClient
    {
        private const string CONSUMER_KEY = null;
        private const string TOKEN = "Bearer";
        private const string TOKEN_SECRET = "fLcXU3tnQ3oATeHX1xmbmlyTq_q6TKLWHZ6OKSNXXLTYun9MtTQNbrlcMMaFw6RKeOYsfti1SA9pdyb_xYWnMCv95AMt8byMCoxWurlZ4X1SYkCeGlNIZ_evVLMxXXYx";
        private const string API_HOST = "https://api.yelp.com";
        private const string BUSINESS_PATH = "/v3/business/search";
         /// <summary>
        /// Prepares OAuth authentication and sends the request to the API.
        /// </summary>
        /// <param name="baseURL">The base URL of the API.</param>
        /// <param name="queryParams">The set of query parameters.</param>
        /// <returns>The JSON response from the API.</returns>
        /// <exception>Throws WebException if there is an error from the HTTP request.</exception>
    //     private JObject PerformRequest(string baseURL, Dictionary<string, string> queryParams=null)
    //     {
    //         var query = System.Web.HttpUtility.ParseQueryString(String.Empty);

    //         if (queryParams == null)
    //         {
    //             queryParams = new Dictionary<string, string>();
    //         }

    //         foreach (var queryParam in queryParams)
    //         {
    //             query[queryParam.Key] = queryParam.Value;
    //         }

    //         var uriBuilder = new UriBuilder(baseURL);
    //         uriBuilder.Query = query.ToString();

    //         var request = WebRequest.Create(uriBuilder.ToString());
    //         request.Method = "GET";

    //         request.SignRequest(
    //             new Tokens {
    //                 ConsumerKey = CONSUMER_KEY,
    //                 ConsumerSecret = CONSUMER_SECRET,
    //                 AccessToken = TOKEN,
    //                 AccessTokenSecret = TOKEN_SECRET
    //             }
    //         ).WithEncryption(EncryptionMethod.HMACSHA1).InHeader();

    //         HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //         var stream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
    //         return JObject.Parse(stream.ReadToEnd());
    //     }
    // }
    public class HomeController : Controller
    {
        private MyContext dbContext;

        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        // API
        // protected void Page_Load()
        // {
        //     // grab logged in user
        //     int? IntVariable = HttpContext.Session.GetInt32("LoggedInId");
        //     var LoggedInUser = dbContext.Users
        //     .Include(user => user.TopCharacter)
        //     .FirstOrDefault(u => u.RegUserId == Convert.ToInt32(IntVariable));

        //     string location = LoggedInUser.Location;
        //     string cuisine = LoggedInUser.TopCharacter.Cuisine;
        //     int price = LoggedInUser.Price;

        //     using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://api.yelp.com/v3"))
        //     {
        //         requestMessage.Headers.Authorization =
        //             new AuthenticationHeaderValue("Bearer", "fLcXU3tnQ3oATeHX1xmbmlyTq_q6TKLWHZ6OKSNXXLTYun9MtTQNbrlcMMaFw6RKeOYsfti1SA9pdyb_xYWnMCv95AMt8byMCoxWurlZ4X1SYkCeGlNIZ_evVLMxXXYx");
        //         HttpClient.SendAsync(requestMessage);
        //     }

        //     string strurltest = string.Format("https://api.yelp.com/v3/businesses/search?term=60647&categories=taco&price=2");
        //     WebRequest requestObjGet = WebRequest.Create(strurltest);
        //     requestObjGet.Method = "GET";
        //     HttpWebResponse responseObjGet = null;
        //     responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();

        //     string strresulttest = null;
        //     using (Stream stream = responseObjGet.GetResponseStream())
        //     {
        //         StreamReader sr = new StreamReader(stream);
        //         strresulttest = sr.ReadToEnd();
        //         sr.Close();
        //     }
        // }

        // public void Do()
        // {
        //     // grab logged in user
        //     int? IntVariable = HttpContext.Session.GetInt32("LoggedInId");
        //     var LoggedInUser = dbContext.Users
        //     .Include(user => user.TopCharacter)
        //     .FirstOrDefault(u => u.RegUserId == Convert.ToInt32(IntVariable));

        //     var result = Call(LoggedInUser.Location, LoggedInUser.TopCharacter.Cuisine, LoggedInUser.Price).Result;
        //     Console.WriteLine(result);
        // }
        // public static async Task<string> Call(string location, string cuisine, int price)
        // {
        //     Console.WriteLine(location);
        //     Console.WriteLine(cuisine);
        //     Console.WriteLine(price);
        //     var url = $"https://api.yelp.com/v3/businesses/search?term={location}&categories={cuisine}&price={price}";
        //     using (var client = new HttpClient())
        //     {
        //         client.BaseAddress = new Uri(url);

        //         // Add an Accept header for JSON format.
        //         client.DefaultRequestHeaders.Accept.Add(
        //         new MediaTypeWithQualityHeaderValue("application/json"));

        //         HttpResponseMessage response = await client.GetAsync(url);
        //         if(response.IsSuccessStatusCode)
        //         {
        //             string strResult = await response.Content.ReadAsStringAsync();
        //             Console.WriteLine(strResult);
        //             return strResult;
        //         }
        //         else
        //         {
        //             Console.WriteLine("It didn't work.");
        //             return null;
        //         }
        //     }
        // }

        [HttpGet("api/yelp/testing")]
        public IActionResult TextYelp()
        {
            var json = new WebClient().DownloadString("https://api.yelp.com/v3/businesses/search?term=60647&categories=taco&price=2");
            Console.WriteLine(json);
            return Json(json);
        }



        // Display Index Page
        [HttpGet("")]
        public ViewResult Index()
        {
            return View("Index");
        }

        // Create RegUser POST route
        [HttpPost("create/register")]
        public IActionResult CreateRegUser(RegUser newUser)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                }
                else
                {
                    PasswordHasher<RegUser> Hasher = new PasswordHasher<RegUser>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    dbContext.Add(newUser);
                    dbContext.SaveChanges();
                    HttpContext.Session.SetInt32("LoggedInId", newUser.RegUserId);
                    return RedirectToAction("Show");
                }
            }
            else
            {
                return View("Index");
            }
        }

        // Login LogUser POST route
        [HttpPost("login")]
        public IActionResult CreateLogUser(LogUser LoggedIn)
        {
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == LoggedIn.LogEmail);
                if(userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Index");
                }
                var hasher = new PasswordHasher<LogUser>();
                var result = hasher.VerifyHashedPassword(LoggedIn, userInDb.Password, LoggedIn.LogPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("LoggedInId", userInDb.RegUserId);
                return RedirectToAction("Show");
            }
            else
            {
                return View("Index");
            }
        }

        // Log Out
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        // Quiz Display Page
        [HttpGet("quiz")]
        public IActionResult Quiz()
        {
            int? IntVariable = HttpContext.Session.GetInt32("LoggedInId");
            if (IntVariable == null)
            {
                HttpContext.Session.Clear();
                ModelState.AddModelError("LoggedIn", "Please log in");
                return RedirectToAction("Index");
            }
            else
            {
                // grab logged in user
                var LoggedInUser = dbContext.Users
                .Include(user => user.TopCharacter)
                .Include(d => d.RestaurantsLiked)
                .FirstOrDefault(u => u.RegUserId == Convert.ToInt32(IntVariable));

                // grab all likes
                var AllLikes = dbContext.Likes
                .Include(w => w.Liker)
                .Include(b => b.RestaurantLiked)
                .ToList();

                ViewBag.LoggedInUser = LoggedInUser;
                return View("Quiz", AllLikes);
            }
        }

        // Quiz Post data
        [HttpPost("quiz/create")]
        public IActionResult CreateQuiz(int Answer1, int Answer2, int Answer3, int Answer4, int Answer5)
        {
            // grab logged in user
            int? IntVariable = HttpContext.Session.GetInt32("LoggedInId");
            var LoggedInUser = dbContext.Users
            .Include(user => user.TopCharacter)
            .Include(d => d.RestaurantsLiked)
            .FirstOrDefault(u => u.RegUserId == Convert.ToInt32(IntVariable));

            int Belle = 0;
            int Mulan = 0;
            int Rapunzel = 0;
            int Ariel = 0;
            int Jasmine = 0;
            int Pocahontas = 0;
            int Tiana = 0;
            int Cinderella = 0;
            int Elsa = 0;
            int Aurora = 0;

            // Calculate Answer to Question #1
            if (Answer1 == 1)
            {
                Belle += 1;
                Tiana += 1;
            }
            else if (Answer1 == 2)
            {
                Mulan += 1;
                Rapunzel += 1;
            }
            else if (Answer1 == 3)
            {
                Ariel += 1;
                Cinderella += 1;
            }
            else if (Answer1 == 4)
            {
                Jasmine += 1;
                Elsa += 1;
            }
            else if (Answer1 == 5)
            {
                Pocahontas += 1;
                Aurora += 1;
            }

            // Calculate Answer to Question #2
            if (Answer2 == 1)
            {
                Belle += 1;
                Rapunzel += 1;
            }
            else if (Answer2 == 2)
            {
                Mulan += 1;
                Ariel += 1;
            }
            else if (Answer2 == 3)
            {
                Pocahontas += 1;
                Cinderella += 1;
            }
            else if (Answer2 == 4)
            {
                Tiana += 1;
                Elsa += 1;
            }
            else if (Answer2 == 5)
            {
                Jasmine += 1;
                Aurora += 1;
            }

            // Calculate Answer to Question #3
            if (Answer3 == 1)
            {
                Belle += 1;
                Cinderella += 1;
            }
            else if (Answer3 == 2)
            {
                Rapunzel += 1;
                Ariel += 1;
            }
            else if (Answer3 == 3)
            {
                Mulan += 1;
                Elsa += 1;
            }
            else if (Answer3 == 4)
            {
                Jasmine += 1;
                Pocahontas += 1;
            }
            else if (Answer3 == 5)
            {
                Tiana += 1;
                Aurora += 1;
            }

            // Calculate Answer to Question #4
            if (Answer4 == 1)
            {
                Belle += 1;
                Jasmine += 1;
            }
            else if (Answer4 == 2)
            {
                Rapunzel += 1;
                Elsa += 1;
            }
            else if (Answer4 == 3)
            {
                Ariel += 1;
                Tiana += 1;
            }
            else if (Answer4 == 4)
            {
                Mulan += 1;
                Cinderella += 1;
            }
            else if (Answer4 == 5)
            {
                Pocahontas += 1;
                Aurora += 1;
            }

            // Calculate Answer to Question #5
            if (Answer5 == 1)
            {
                Mulan += 1;
                Jasmine += 1;
            }
            else if (Answer5 == 2)
            {
                Pocahontas += 1;
                Tiana += 1;
            }
            else if (Answer5 == 3)
            {
                Belle += 1;
                Rapunzel += 1;
            }
            else if (Answer5 == 4)
            {
                Cinderella += 1;
                Elsa += 1;
            }
            else if (Answer5 == 5)
            {
                Ariel += 1;
                Aurora += 1;
            }

            Console.WriteLine("Belle = " + Belle);
            Console.WriteLine("Mulan = " + Mulan);
            Console.WriteLine("Rapunzel = " + Rapunzel);
            Console.WriteLine("Ariel = " + Ariel);
            Console.WriteLine("Jasmine = " + Jasmine);
            Console.WriteLine("Pocahontas = " + Pocahontas);
            Console.WriteLine("Tiana = " + Tiana);
            Console.WriteLine("Cinderella = " + Cinderella);
            Console.WriteLine("Elsa = " + Elsa);
            Console.WriteLine("Aurora = " + Aurora);

            Dictionary<string, int> Results = new Dictionary<string, int>();
            Results.Add("Belle", Belle);
            Results.Add("Mulan", Mulan);
            Results.Add("Rapunzel", Rapunzel);
            Results.Add("Ariel", Ariel);
            Results.Add("Jasmine", Jasmine);
            Results.Add("Pocahontas", Pocahontas);
            Results.Add("Tiana", Tiana);
            Results.Add("Cinderella", Cinderella);
            Results.Add("Elsa", Elsa);
            Results.Add("Aurora", Aurora);

            // Function for sorting a dictionary
            List<KeyValuePair<string, int>> Sorted = (from kv in Results orderby kv.Value descending select kv).ToList();

            int i = 0;

            foreach (KeyValuePair<string, int> kv in Sorted)
            {
                if (i == 0)
                {
                    Console.WriteLine("Top Character = " + kv.Key);
                    LoggedInUser.TopCharacter = dbContext.Characters
                    .FirstOrDefault(c => c.Name == kv.Key);
                    dbContext.SaveChanges();
                    i++;
                }
                else if (i == 1)
                {
                    Console.WriteLine("Second Character = " + kv.Key);
                    LoggedInUser.SecondCharacter = kv.Key;
                    dbContext.SaveChanges();
                    i++;
                }
                else if (i == 2)
                {
                    Console.WriteLine("Third Character = " + kv.Key);
                    LoggedInUser.ThirdCharacter = kv.Key;
                    dbContext.SaveChanges();
                    i++;
                }
                else
                {
                    break;
                }
            }

            return RedirectToAction("Show");
        }

        // Swipe Display Page
        // [HttpGet("swipe")]
        // public IActionResult Swipe()
        // {
        //     Page_Load();
        //     return View("Show");
        // }

        [HttpGet("show")]
        public IActionResult Show()
        {
            return View("Show");
        }

    }
    }
}
