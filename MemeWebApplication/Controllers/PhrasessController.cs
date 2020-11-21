using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemeWebApplication.AppDbContext;
using MemeWebApplication.Models;
using System.Dynamic;

namespace MemeWebApplication.Controllers
{
    public class PhrasessController : Controller
    {
        private readonly MemeDbContext _context;

        public PhrasessController(MemeDbContext context)
        {
            _context = context;
        }

        // GET: Phrases
        public async Task<IActionResult> Index()
        {
            var memeDbContext = _context.Phrases.Include(p => p.Template);
            return View(await memeDbContext.ToListAsync());
        }

        // GET: Phrases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phrase = await _context.Phrases
                .Include(p => p.Template)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (phrase == null)
            {
                return NotFound();
            }

            return View(phrase);
        }

        // GET: Phrases/Create
        public IActionResult Create()
        {
            ViewData["TemplateID"] = new SelectList(_context.Templates, "ID", "FilmName");
            return View();
        }

        // POST: Phrases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Text,TemplateID")] Phrase phrase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phrase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TemplateID"] = new SelectList(_context.Templates, "ID", "FilmName", phrase.TemplateID);
            return View(phrase);
        }

        // GET: Phrases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phrase = await _context.Phrases.FindAsync(id);
            if (phrase == null)
            {
                return NotFound();
            }
            ViewData["TemplateID"] = new SelectList(_context.Templates, "ID", "FilmName", phrase.TemplateID);
            return View(phrase);
        }

        // POST: Phrases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Text,TemplateID")] Phrase phrase)
        {
            if (id != phrase.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phrase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhraseExists(phrase.ID))
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
            ViewData["TemplateID"] = new SelectList(_context.Templates, "ID", "FilmName", phrase.TemplateID);
            return View(phrase);
        }

        // GET: Phrases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phrase = await _context.Phrases
                .Include(p => p.Template)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (phrase == null)
            {
                return NotFound();
            }

            return View(phrase);
        }

        // POST: Phrases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phrase = await _context.Phrases.FindAsync(id);
            _context.Phrases.Remove(phrase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhraseExists(int id)
        {
            return _context.Phrases.Any(e => e.ID == id);
        }
        public IActionResult SearchbyPhrase(string SearchString)
        {
            var result = (from c in _context.Phrases
                          select c).ToList();

            if (!string.IsNullOrEmpty(SearchString))
            {
                result = (_context.Phrases.Where(x => x.Text.Contains(SearchString))).ToList();

            }
           
            return View(result.ToList());

        }
    }
}
