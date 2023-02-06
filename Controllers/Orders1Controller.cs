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
    public class Orders1Controller : Controller
    {
        private readonly ModelContext _context;

        public Orders1Controller(ModelContext context)
        {
            _context = context;
        }

        // GET: Orders1
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Orders1s.Include(o => o.Pro).Include(o => o.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Orders1/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders1 = await _context.Orders1s
                .Include(o => o.Pro)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders1 == null)
            {
                return NotFound();
            }

            return View(orders1);
        }

        // GET: Orders1/Create
        public IActionResult Create()
        {
            ViewData["Proid"] = new SelectList(_context.Product1s, "Id", "Id");
            ViewData["Userid"] = new SelectList(_context.Users1s, "Id", "Id");
            return View();
        }

        // POST: Orders1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,Datefrom,Dateto,Stats,Proid,Userid")] Orders1 orders1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Proid"] = new SelectList(_context.Product1s, "Id", "Id", orders1.Proid);
            ViewData["Userid"] = new SelectList(_context.Users1s, "Id", "Id", orders1.Userid);
            return View(orders1);
        }

        // GET: Orders1/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders1 = await _context.Orders1s.FindAsync(id);
            if (orders1 == null)
            {
                return NotFound();
            }
            ViewData["Proid"] = new SelectList(_context.Product1s, "Id", "Id", orders1.Proid);
            ViewData["Userid"] = new SelectList(_context.Users1s, "Id", "Id", orders1.Userid);
            return View(orders1);
        }

        // POST: Orders1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Quantity,Datefrom,Dateto,Stats,Proid,Userid")] Orders1 orders1)
        {
            if (id != orders1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Orders1Exists(orders1.Id))
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
            ViewData["Proid"] = new SelectList(_context.Product1s, "Id", "Id", orders1.Proid);
            ViewData["Userid"] = new SelectList(_context.Users1s, "Id", "Id", orders1.Userid);
            return View(orders1);
        }

        // GET: Orders1/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders1 = await _context.Orders1s
                .Include(o => o.Pro)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders1 == null)
            {
                return NotFound();
            }

            return View(orders1);
        }

        // POST: Orders1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var orders1 = await _context.Orders1s.FindAsync(id);
            _context.Orders1s.Remove(orders1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Orders1Exists(decimal id)
        {
            return _context.Orders1s.Any(e => e.Id == id);
        }
    }
}
