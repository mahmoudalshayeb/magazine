using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using magazine.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace magazine.Controllers
{
    public class Product1Controller : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public Product1Controller(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: Product1
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Product1s.Include(p => p.Cat);
            return View(await modelContext.ToListAsync());
        }

        // GET: Product1/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product1 = await _context.Product1s
                .Include(p => p.Cat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product1 == null)
            {
                return NotFound();
            }

            return View(product1);
        }

        // GET: Product1/Create
        public IActionResult Create()
        {
            ViewData["Catid"] = new SelectList(_context.Categories, "Name", "Name");
            return View();
        }

        // POST: Product1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Sale,Rate,Catid,ImgPath,ImageFile")] Product1 product1)
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
               
                _context.Add(product1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Catid"] = new SelectList(_context.Categories, "Name", "Name", product1.Name);
            return View(product1);
        }

        // GET: Product1/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
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

        // POST: Product1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Price,Sale,Rate,Catid,ImgPath")] Product1 product1)
        {
            if (id != product1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Product1Exists(product1.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Catid"] = new SelectList(_context.Categories, "Id", "Id", product1.Catid);
            return View(product1);
        }

        // GET: Product1/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product1 = await _context.Product1s
                .Include(p => p.Cat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product1 == null)
            {
                return NotFound();
            }

            return View(product1);
        }

        // POST: Product1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var product1 = await _context.Product1s.FindAsync(id);
            _context.Product1s.Remove(product1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Product1Exists(decimal id)
        {
            return _context.Product1s.Any(e => e.Id == id);
        }
    }
}
