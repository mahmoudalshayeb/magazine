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
    public class Roles1Controller : Controller
    {
        private readonly ModelContext _context;

        public Roles1Controller(ModelContext context)
        {
            _context = context;
        }

        // GET: Roles1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles1s.ToListAsync());
        }

        // GET: Roles1/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles1 = await _context.Roles1s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roles1 == null)
            {
                return NotFound();
            }

            return View(roles1);
        }

        // GET: Roles1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Roles1 roles1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roles1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roles1);
        }

        // GET: Roles1/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles1 = await _context.Roles1s.FindAsync(id);
            if (roles1 == null)
            {
                return NotFound();
            }
            return View(roles1);
        }

        // POST: Roles1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name")] Roles1 roles1)
        {
            if (id != roles1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roles1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Roles1Exists(roles1.Id))
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
            return View(roles1);
        }

        // GET: Roles1/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles1 = await _context.Roles1s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roles1 == null)
            {
                return NotFound();
            }

            return View(roles1);
        }

        // POST: Roles1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var roles1 = await _context.Roles1s.FindAsync(id);
            _context.Roles1s.Remove(roles1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Roles1Exists(decimal id)
        {
            return _context.Roles1s.Any(e => e.Id == id);
        }
    }
}
