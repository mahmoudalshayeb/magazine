using magazine.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Asn1.X509;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using IronPdf;
using Microsoft.AspNetCore.Components.RenderTree;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Crypto.Tls;

namespace magazine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly ModelContext _context;

		public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.name= HttpContext.Session.GetString("name");
            var user = _context.Users1s.ToList();
            var category = _context.Categories.ToList();
            var product = _context.Product1s.ToList();
            var order = _context.Orders1s.ToList();
            var testmonial = _context.Testimonials.Where(x=>x.State==1).ToList();

            var models = Tuple.Create<IEnumerable<Users1>, IEnumerable<Category>, IEnumerable<Orders1>, IEnumerable<Testimonial>, IEnumerable<Product1>>(user, category,order,testmonial,product);

            return View(models);

        }
		public IActionResult ContactUs()
		{
			ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
			ViewBag.name = HttpContext.Session.GetString("name");
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ContactUsHome(string name, string email , string text)
		{

			Contactu contactu = new Contactu();
            contactu.Name = name;
            contactu.Email = email;
            contactu.Text = text;
			_context.Add(contactu);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs([Bind("Name,Email,Text")] Contactu contactu )
        {
            if (ModelState.IsValid)
            {


                
                _context.Add(contactu);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View(contactu);
        }

        public IActionResult AboutUs()
        {
           
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
			ViewBag.name = HttpContext.Session.GetString("name");
            return View(_context.Aboutus.FirstOrDefault());
        }

        public IActionResult Cart()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
			ViewBag.name = HttpContext.Session.GetString("name");
			var data = _context.Orders1s.Include(x => x.Pro).Where(x=>x.Stats==0);
            return View(data);
        }

        
       
        public IActionResult Delete(decimal id)
        {
            var orders1 = _context.Orders1s.Find(id);
            _context.Orders1s.Remove(orders1);
             _context.SaveChangesAsync();
            return RedirectToAction(nameof(Cart));
		}


        public IActionResult Shop(string SearchString)
        {
            if (string.IsNullOrEmpty(SearchString)) { 
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.name = HttpContext.Session.GetString("name");
            var products = _context.Product1s.ToList();
            var Category = _context.Categories.ToList();
            var data = Tuple.Create<IEnumerable<Product1>, IEnumerable<Category>>(products, Category);

            return View(data);
        }
            else
            {
				ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
				ViewBag.name = HttpContext.Session.GetString("name");
				var products = _context.Product1s.Where(x=>x.Name.Contains(SearchString));
				var Category = _context.Categories.ToList();
				var data = Tuple.Create<IEnumerable<Product1>, IEnumerable<Category>>(products, Category);
                return View(data);
			}
        }

		


		public IActionResult ADD(decimal id)
        {
           
            var userId= HttpContext.Session.GetInt32("UserId");

            Orders1 order1 = new Orders1();
            order1.Proid = id;
            order1.Userid = userId;
            order1.Quantity = 1;
            order1.Stats = 0;
            order1.Datefrom = DateTime.Now;

            _context.Add(order1);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Shop));
            
        }

        
      


        public IActionResult Checkout()
        {
            var user= HttpContext.Session.GetInt32("UserId");
            var test=_context.Orders1s.Where(x=>x.Userid == user && x.Stats==0).ToList();
            var email= HttpContext.Session.GetString("email");
			var order = _context.Orders1s.Include(x => x.Pro).ToList();
            var balance = 3000;
            decimal total = 0;
            var theOrder = " ";
            
            foreach (var item in test)
            {
                total += Convert.ToDecimal(item.Quantity * item.Pro.Price);
                theOrder += item.Pro.Name + "||";
               
            };
            if(total<balance)
            { 
            foreach(var item in test)
            {
                item.Stats = 1;
                    _context.Update(item);
                    _context.SaveChangesAsync();
                }


            



                var Renderer = new ChromePdfRenderer();
                var pdf = Renderer.RenderHtmlAsPdf($" <h1> The Total is : $ {total}  </h1> \n <h1> <p> Product count is :{test.Count()} </p> Product names : {theOrder}  </h1>");
                pdf.SaveAs("Imvoce.pdf");

                string x = "Thank you for purchasing from our website. We hope you like our service";
                
            MimeMessage message= new MimeMessage();
                message.From.Add(new MailboxAddress("Store", "mahmoudahmad256@gmail.com"));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = "Invoice";
                var builder = new BodyBuilder();
                builder.TextBody = x;
                builder.HtmlBody = "<p> Thank you for purchasing from our website. We hope you like our service </p>";
                builder.Attachments.Add(@"G:\magazine\Imvoce.pdf");

                message.Body = builder.ToMessageBody();
               

                string emailaddress = "mahmoudahmad256@gmail.com";
                string password = "wsqmdllxdjowtpql";
                
                SmtpClient client=new SmtpClient();
                try
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate(emailaddress, password);
                    client.Send(message);

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }


            }
            return RedirectToAction(nameof(Index));
        }


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Password,Firstname,Lastname,Email,Phonenumber")] string username ,Users1 users1)
        {
            var u = _context.Users1s.Where(x=>x.Username==username).FirstOrDefault() ;
           

                if (ModelState.IsValid)
                {


                    users1.Roleid = 2;
                    users1.Username = username;
                    _context.Add(users1);
                    await _context.SaveChangesAsync();
                    

                    return RedirectToAction("Login", "Home");
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
                        HttpContext.Session.SetString("name", auth.Firstname);
                        return RedirectToAction("index", "Admin");
                    case 2:
                        HttpContext.Session.SetString("Firstname", auth.Firstname);
                        HttpContext.Session.SetInt32("UserId", Convert.ToInt32(auth.Id));
						HttpContext.Session.SetString("name", auth.Firstname);
                        HttpContext.Session.SetString("email", auth.Email);
                        return RedirectToAction("index", "Home");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
			return RedirectToAction("index", "Home");
		}

		

        public IActionResult Profile()
        {
            ViewBag.UserId= HttpContext.Session.GetInt32("UserId");
			ViewBag.name = HttpContext.Session.GetString("name");
			var z = HttpContext.Session.GetInt32("UserId");
			var profileData = _context.Users1s.Where(x=>x.Id==z);
            return View(profileData);

        }

        public IActionResult MyOrders()
        {

            ViewBag.UserId= HttpContext.Session.GetInt32("UserId");
            var z= HttpContext.Session.GetInt32("UserId");
            var data = _context.Orders1s.Include(x => x.Pro).Where(x => x.Stats != 0);
            return  View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(string Firstname, string Lastname,string Password, decimal Phonenumber)
        {
            var UserId = HttpContext.Session.GetInt32("UserId");
           
            if (ModelState.IsValid)
            {
                var profile = _context.Users1s.Where(z => z.Id == UserId).FirstOrDefault();

               

                if (profile.Firstname != Firstname)
                {
                    profile.Firstname = Firstname;
                }
                if (profile.Lastname != Lastname)
                {
                    profile.Lastname = Lastname;
                }
                if (profile.Password != Password)
                {
                    profile.Password = Password;
                }

                
                if(profile.Phonenumber!= Phonenumber)
                {
                    profile.Phonenumber = Phonenumber;  
                }




                _context.Update(profile);

                _context.SaveChangesAsync();
                return RedirectToAction("Profile", "Home");
            }
            return NotFound();
        }

        public IActionResult AddTestmonial()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTestmonial([Bind("Id,Text")] Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                testimonial.State = 0;
                testimonial.Userid = HttpContext.Session.GetInt32("UserId");
                _context.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(testimonial);
        }

		
	}
}

