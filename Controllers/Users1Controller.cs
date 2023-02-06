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
    public class Users1Controller : Controller
    {
        private readonly ModelContext _context;

        public Users1Controller(ModelContext context)
        {
            _context = context;
        }

        // GET: Users1
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Users1s.Include(u => u.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: Users1/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users1 = await _context.Users1s
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users1 == null)
            {
                return NotFound();
            }

            return View(users1);
        }

        // GET: Users1/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles1s, "Id", "Id");
            return View();
        }

        // POST: Users1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Firstname,Lastname,Email,Review,Phonenumber,Roleid")] Users1 users1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Roles1s, "Id", "Id", users1.Roleid);
            return View(users1);
        }

        // GET: Users1/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users1 = await _context.Users1s.FindAsync(id);
            if (users1 == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles1s, "Id", "Id", users1.Roleid);
            return View(users1);
        }

        // POST: Users1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Username,Password,Firstname,Lastname,Email,Review,Phonenumber,Roleid")] Users1 users1)
        {
            if (id != users1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Users1Exists(users1.Id))
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
            ViewData["Roleid"] = new SelectList(_context.Roles1s, "Id", "Id", users1.Roleid);
            return View(users1);
        }

        // GET: Users1/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users1 = await _context.Users1s
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users1 == null)
            {
                return NotFound();
            }

            return View(users1);
        }

        // POST: Users1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var users1 = await _context.Users1s.FindAsync(id);
            _context.Users1s.Remove(users1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Users1Exists(decimal id)
        {
            return _context.Users1s.Any(e => e.Id == id);
        }
    }
}
