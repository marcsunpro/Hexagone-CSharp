using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shout.DAL;
using Shout.Models;

namespace Shout.Controllers
{
    public class ShouttController : Controller
    {
        private readonly ShoutContext _context;

        public ShouttController()
        {
            _context = new ShoutContext();
        }

        // GET: Shoutt
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shoutts.ToListAsync());
        }

        // GET: Shoutt/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoutt = await _context.Shoutts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoutt == null)
            {
                return NotFound();
            }

            return View(shoutt);
        }

        // GET: Shoutt/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shoutt/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,DatePublication")] Shoutt shoutt)
        {
            if (ModelState.IsValid)
            {
                shoutt.DatePublication = DateTime.Now;
                _context.Add(shoutt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoutt);
        }

        // GET: Shoutt/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoutt = await _context.Shoutts.FindAsync(id);
            if (shoutt == null)
            {
                return NotFound();
            }
            return View(shoutt);
        }

        // POST: Shoutt/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,DatePublication")] Shoutt shoutt)
        {
            if (id != shoutt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoutt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShouttExists(shoutt.Id))
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
            return View(shoutt);
        }

        // GET: Shoutt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoutt = await _context.Shoutts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoutt == null)
            {
                return NotFound();
            }

            return View(shoutt);
        }

        // POST: Shoutt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoutt = await _context.Shoutts.FindAsync(id);
            _context.Shoutts.Remove(shoutt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShouttExists(int id)
        {
            return _context.Shoutts.Any(e => e.Id == id);
        }
    }
}
