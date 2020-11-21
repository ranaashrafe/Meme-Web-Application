using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemeWebApplication.AppDbContext;
using MemeWebApplication.Models;

namespace MemeWebApplication.Controllers
{
    public class ActorTemplatesssController : Controller
    {
        private readonly MemeDbContext _context;

        public ActorTemplatesssController(MemeDbContext context)
        {
            _context = context;
        }

        // GET: ActorTemplates
      /*  public async Task<IActionResult> Index()
        {
            return View(await _context.ActorTemplates.ToListAsync());
        }

        // GET: ActorTemplates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorTemplates = await _context.ActorTemplates
                .FirstOrDefaultAsync(m => m.ID == id);
            if (actorTemplates == null)
            {
                return NotFound();
            }

            return View(actorTemplates);
        }

        // GET: ActorTemplates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActorTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID")] ActorTemplates actorTemplates)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actorTemplates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actorTemplates);
        }

        // GET: ActorTemplates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorTemplates = await _context.ActorTemplates.FindAsync(id);
            if (actorTemplates == null)
            {
                return NotFound();
            }
            return View(actorTemplates);
        }

        // POST: ActorTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID")] ActorTemplates actorTemplates)
        {
            if (id != actorTemplates.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actorTemplates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorTemplatesExists(actorTemplates.ID))
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
            return View(actorTemplates);
        }

        // GET: ActorTemplates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorTemplates = await _context.ActorTemplates
                .FirstOrDefaultAsync(m => m.ID == id);
            if (actorTemplates == null)
            {
                return NotFound();
            }

            return View(actorTemplates);
        }

        // POST: ActorTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorTemplates = await _context.ActorTemplates.FindAsync(id);
            _context.ActorTemplates.Remove(actorTemplates);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorTemplatesExists(int id)
        {
            return _context.ActorTemplates.Any(e => e.ID == id);
        }*/
    }
}
