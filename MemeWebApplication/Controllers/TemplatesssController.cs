using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemeWebApplication.AppDbContext;
using MemeWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MemeWebApplication.Controllers
{
    public class TemplatesssController : Controller
    {
        private readonly MemeDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public TemplatesssController(MemeDbContext context, IHostingEnvironment hosting)
        {
            _context = context;
            hostingEnvironment = hosting;
        }

        // GET: Templates
        public async Task<IActionResult> Index()
        {
            return View(await _context.Templates.ToListAsync());
        }

        // GET: Templates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates
                .FirstOrDefaultAsync(m => m.ID == id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // GET: Templates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Templates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("AddTemplate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FilmName,Image")] Template template)
        {
            if (ModelState.IsValid)
            {
                var FileNaame = HttpContext.Request.Form.Files;
                foreach (var Image in FileNaame)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                        if (file.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                template.Image = fileName;

                            }
                        }
                    }
                }



                _context.Add(template);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(template);
        }

        // GET: Templates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }
            return View(template);
        }

        // POST: Templates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FilmName,Image")] Template template)
        {
            if (id != template.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(template);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemplateExists(template.ID))
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
            return View(template);
        }

        // GET: Templates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates
                .FirstOrDefaultAsync(m => m.ID == id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // POST: Templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var template = await _context.Templates.FindAsync(id);
            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemplateExists(int id)
        {
            return _context.Templates.Any(e => e.ID == id);
        }
        // GET: Phrases/Create
        public IActionResult CreatePhrase()
        {
            return View();
        }

        // POST: Phrases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("CreatePhrase")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePhrase([Bind("Text")] Phrase phrase, int? id)
        {
            if (ModelState.IsValid)
            {
                phrase.Template = _context.Templates.Find(id);



                _context.Add(phrase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phrase);
        }
        // GET: Actors/Create
        public IActionResult CreateActor()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
       [HttpPost("CreateActor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateActor([Bind("Name")] Actor actor, int? id)
        {
            if (ModelState.IsValid)
            {
                //actor.ActorTemp = (_context.Templates.Find(id)).ActorTemp;

                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        public IActionResult SearchbyFilm(string SearchString)
        {
            var result = (from c in _context.Templates
                          select c).ToList();

            if (!string.IsNullOrEmpty(SearchString))
            {
                result = (_context.Templates.Where(x => x.FilmName.Contains(SearchString))).ToList();

            }
            return View(result.ToList());

        }
    }
}
