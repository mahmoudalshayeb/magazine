using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using magazine.Models;

namespace magazine.Controllers
{
    public class ElementsController : Controller
    {
        private readonly ModelContext _context;

        public ElementsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Elements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Elements.ToListAsync());
        }

        // GET: Elements/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var element = await _context.Elements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (element == null)
            {
                return NotFound();
            }

            return View(element);
        }

        // GET: Elements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Elements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Backgroudcolor,Aboutustest,Footertext1,Footertext2,Footertext3,Logoimg,Sliderpath1,Sliderpath2,Sliderpath3,Phonenumber")] Element element)
        {
            if (ModelState.IsValid)
            {
                _context.Add(element);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(element);
        }

        // GET: Elements/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var element = await _context.Elements.FindAsync(id);
            if (element == null)
            {
                return NotFound();
            }
            return View(element);
        }

        // POST: Elements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Backgroudcolor,Aboutustest,Footertext1,Footertext2,Footertext3,Logoimg,Sliderpath1,Sliderpath2,Sliderpath3,Phonenumber")] Element element)
        {
            if (id != element.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(element);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElementExists(element.Id))
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
            return View(element);
        }

        // GET: Elements/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var element = await _context.Elements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (element == null)
            {
                return NotFound();
            }

            return View(element);
        }

        // POST: Elements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var element = await _context.Elements.FindAsync(id);
            _context.Elements.Remove(element);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElementExists(decimal id)
        {
            return _context.Elements.Any(e => e.Id == id);
        }
    }
}
