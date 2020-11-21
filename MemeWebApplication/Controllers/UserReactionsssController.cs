using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemeWebApplication.AppDbContext;
using MemeWebApplication.Models;
using System.IO;

namespace MemeWebApplication.Controllers
{
    public class UserReactionsssController : Controller
    {
        private readonly MemeDbContext _context;

        public UserReactionsssController(MemeDbContext context)
        {
            _context = context;
        }

        // GET: UserReactions
        public async Task<IActionResult> Index()
        {
            var memeDbContext = _context.UserReactions.Include(u => u.Template).Include(u => u.User);
            return View(await memeDbContext.ToListAsync());
        }

       
        // GET: UserReactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userReaction = await _context.UserReactions
                .Include(u => u.Template)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userReaction == null)
            {
                return NotFound();
            }

            return View(userReaction);
        }

        // GET: UserReactions/Create
        public IActionResult Create()
        {
            ViewData["TemplateID"] = new SelectList(_context.Templates, "ID", "FilmName");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email");
            return View();
        }

        // POST: UserReactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,TemplateID,Favourite,Download")] UserReaction userReaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userReaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TemplateID"] = new SelectList(_context.Templates, "ID", "FilmName", userReaction.TemplateID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", userReaction.UserID);
            return View(userReaction);
        }

        // GET: UserReactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userReaction = await _context.UserReactions.FindAsync(id);
            if (userReaction == null)
            {
                return NotFound();
            }
            ViewData["TemplateID"] = new SelectList(_context.Templates, "ID", "FilmName", userReaction.TemplateID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", userReaction.UserID);
            return View(userReaction);
        }

        // POST: UserReactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,TemplateID,Favourite,Download")] UserReaction userReaction)
        {
            if (id != userReaction.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userReaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserReactionExists(userReaction.ID))
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
            ViewData["TemplateID"] = new SelectList(_context.Templates, "ID", "FilmName", userReaction.TemplateID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", userReaction.UserID);
            return View(userReaction);
        }

        // GET: UserReactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userReaction = await _context.UserReactions
                .Include(u => u.Template)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userReaction == null)
            {
                return NotFound();
            }

            return View(userReaction);
        }

        // POST: UserReactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userReaction = await _context.UserReactions.FindAsync(id);
            _context.UserReactions.Remove(userReaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserReactionExists(int id)
        {
            return _context.UserReactions.Any(e => e.ID == id);
        }
    
    }
}
