using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using magazine.Models;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace magazine.Controllers
{
	public class RegisterController : Controller
	{
		private readonly ModelContext _context;
		private readonly IWebHostEnvironment _webHostEnviroment;

		public RegisterController(ModelContext context, IWebHostEnvironment webHostEnviroment)
		{
			_context = context;
			_webHostEnviroment = webHostEnviroment;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Register()
		{
			return View();
		}

		
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Register([Bind("Password,Firstname,Lastname,Email,Phonenumber")] string username, Users1 users1)
            {
                var u = _context.Users1s.Where(x => x.Username == username).FirstOrDefault();
                if (u == null)
                {

                    if (ModelState.IsValid)
                    {


                        users1.Roleid = 2;
                        users1.Username = username;
                        _context.Add(users1);
                        await _context.SaveChangesAsync();


                        return RedirectToAction("Login", "Register");
                    }
                    else
                    {
                        return RedirectToAction("Register", "Register");
                    }
                }

                return View(users1);
            }

        public JsonResult check(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            var search = _context.Users1s.Where(x => x.Username == userdata).SingleOrDefault();
            if (search != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }


            public IActionResult Login()
        {
            
            return View();

        }


        [HttpPost]
        public async Task<IActionResult> Login([Bind("Username,Password")] Users1 user1)
        {
            var auth = _context.Users1s.Where(x => x.Username == user1.Username && x.Password == user1.Password).SingleOrDefault();

            if (auth != null)
            {
                switch (auth.Roleid)
                {
                    case 1:
                        HttpContext.Session.SetInt32("Userid", Convert.ToInt32(auth.Id));
                        HttpContext.Session.SetString("Username", auth.Username);
                        return RedirectToAction("index", "Admin");
                    case 2:
                        HttpContext.Session.SetString("Firstname", auth.Firstname);
                        HttpContext.Session.SetInt32("UserId", Convert.ToInt32(auth.Id));
                        return RedirectToAction("index", "Home");
                }
            }
            return View();
        }

    }
}
