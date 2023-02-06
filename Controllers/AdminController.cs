using magazine.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace magazine.Controllers
{
	public class AdminController : Controller
	{
		private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public AdminController(ModelContext context, IWebHostEnvironment webHostEnviroment)
		{
			_context = context;
            _webHostEnviroment = webHostEnviroment;
        }
		public IActionResult Index()
		{

            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
			ViewBag.Username =HttpContext.Session.GetString("Username");
            var order = _context.Orders1s.ToList();
            var testmonial = _context.Testimonials.ToList();
            var user =_context.Users1s.ToList();
            var product=_context.Product1s.ToList();
            var category=_context.Categories.ToList();
            var model = Tuple.Create<IEnumerable<Orders1>, IEnumerable<Testimonial>, IEnumerable<Users1>, IEnumerable<Product1>, IEnumerable<Category>>(order, testmonial,user,product,category);
            return View(model);
		}
        [HttpGet]
        public IActionResult Orders()
		{

            var modeldata = _context.Orders1s.Include(p => p.Pro).Include(u => u.User);
            return View(modeldata);
		}
       
        public async Task<IActionResult> Orders(DateTime? stratDate, DateTime? endDate)
        {
            var modelContext = _context.Orders1s.Include(p=>p.Pro).Include(x=>x.User);
            if (stratDate == null && endDate == null)
            {
                return View(modelContext);
            }
            else if (stratDate == null && endDate != null)
            {
                var result = await modelContext.Where(x => x.Datefrom.Value.Date <= endDate).ToListAsync();
                return View(result);
            }
            else if (stratDate != null && endDate == null)
            {
                var result = await modelContext.Where(x => x.Datefrom.Value.Date >= stratDate).ToListAsync();
                return View(result);
            }
            else
            {
                var result = await modelContext.Where(x => x.Datefrom >= stratDate && x.Datefrom <= endDate).ToListAsync();
                return View(result);
            }

        }

        public IActionResult Testmonial()
        {
			var testmonial = _context.Testimonials.ToList();
            

            return View(testmonial);
        }

        
        public IActionResult Accpet(decimal id)
        {
            
            var testmonial = _context.Testimonials.Where(z => z.Id == id).FirstOrDefault();

            if (testmonial == null)
            {
                return NotFound();
            }
            else
            {
                testmonial.State = 1;
                _context.Update(testmonial);
               _context.SaveChangesAsync();
                return RedirectToAction(nameof(Testmonial));
            }
        }

        public IActionResult Ignore(decimal id)
        {
            var testmonial = _context.Testimonials.Where(z => z.Id == id).FirstOrDefault();
            if (testmonial == null)
            {
                return NotFound();
            }
            else
            {
                testmonial.State = 0;
                _context.Update(testmonial);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Testmonial));
            }
        }

		public IActionResult Users()
		{
			var users = _context.Users1s.ToList();
			return View(users);
		}
        public IActionResult Product()
        {
            var product = _context.Product1s.Include(x=>x.Cat).ToList();
            return View(product);
        }

        public IActionResult UnPublishe(decimal id)
        {

            var product = _context.Product1s.Where(z => z.Id == id).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                product.Ispublished = 1;
                _context.Update(product);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Product));
            }
        }

        public IActionResult ContactUs()
        {
            var contact=_context.Contactus.ToList();
            return View(contact);
        }


        public IActionResult Profile()
        {
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            var profileData = _context.Users1s.ToList();
            return View(profileData);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(string Firstname, string Lastname, string Password, decimal Phonenumber)
        {
       
            var UserId = HttpContext.Session.GetInt32("Userid");

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


                if (profile.Phonenumber != Phonenumber)
                {
                    profile.Phonenumber = Phonenumber;
                }




                _context.Update(profile);

                _context.SaveChangesAsync();
                return RedirectToAction("Profile", "Admin");
            }
            return NotFound();
        }

        public IActionResult CreateP()
        {
            ViewData["Catid"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateP([Bind("Id,Name,Price,Sale,Catid,ImgPath,ImageFile")]  Product1 product1)
        {
            if (ModelState.IsValid)
            {
                if (product1.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + product1.ImageFile.FileName;
                    string path = Path.Combine(wwwrootPath + "/images/" + fileName);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await product1.ImageFile.CopyToAsync(filestream);
                        product1.Imgpath = fileName;
                    }

                }
               
             
                product1.Rate= 1;
                _context.Add(product1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Product));
            }
            ViewData["Catid"] = new SelectList(_context.Categories, "Id", "Id", product1.Id);
            return View(product1);
        }

        public async Task<IActionResult> EditP(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product1 = await _context.Product1s.FindAsync(id);
            if (product1 == null)
            {
                return NotFound();
            }
            ViewData["Catid"] = new SelectList(_context.Categories, "Id", "Id", product1.Catid);
            return View(product1);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditP(decimal id, [Bind("Id,Name,Price,Sale,Catid,ImgPath,ImageFile")] Product1 product1)
        {
            if (id != product1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    if (product1.ImageFile != null)
                    {
                        string wwwrootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + product1.ImageFile.FileName;
                        string path = Path.Combine(wwwrootPath + "/images/" + fileName);

                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await product1.ImageFile.CopyToAsync(filestream);
                            product1.Imgpath = fileName;
                        }

                    }
                    _context.Update(product1);
                    await _context.SaveChangesAsync();
                
                
                return RedirectToAction(nameof(Product));
            }
            ViewData["Catid"] = new SelectList(_context.Categories, "Id", "Id", product1.Catid);
            return View(product1);
        }



        public IActionResult DeleteP(decimal id)
        {
            var product = _context.Product1s.Where(z => z.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            else
            {

                _context.Product1s.Remove(product);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Product));
            }
        }

        public IActionResult Category() 
        { 
         var category = _context.Categories.ToList();
         return View(category); 
        }

        public IActionResult DeleteC(decimal id)
        {
            var category = _context.Categories.Where(z => z.Id == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }
            else
            {

                _context.Categories.Remove(category);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Category));
            }
        }

        public IActionResult CreateC()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateC([Bind("Id,Name,ImgPath,ImageFile")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + category.ImageFile.FileName;
                    string path = Path.Combine(wwwrootPath + "/images/" + fileName);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await category.ImageFile.CopyToAsync(filestream);
                        category.Imgpath = fileName;
                    }

                }
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Category));
            }
            
            return View(category);
        }



        public async Task<IActionResult> EditC(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditC(decimal id, [Bind("Id,Name,ImgPath,ImageFile")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (category.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + category.ImageFile.FileName;
                    string path = Path.Combine(wwwrootPath + "/images/" + fileName);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await category.ImageFile.CopyToAsync(filestream);
                        category.Imgpath = fileName;
                    }

                }
                _context.Update(category);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Category));
            }
           
            return View(category);
        }




    }
}
