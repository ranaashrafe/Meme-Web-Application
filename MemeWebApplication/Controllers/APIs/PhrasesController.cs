using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemeWebApplication.AppDbContext;
using MemeWebApplication.Models;
using System.Collections.ObjectModel;

namespace MemeWebApplication.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhrasesController : ControllerBase
    {
        private readonly MemeDbContext _context;

        public PhrasesController(MemeDbContext context)
        {
            _context = context;
        }

        // GET: api/Phrases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phrase>>> GetPhrases()
        {
            return await _context.Phrases.ToListAsync();
        }

        // GET: api/Phrases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Phrase>> GetPhrase(int id)
        {
            var phrase = await _context.Phrases.FindAsync(id);

            if (phrase == null)
            {
                return NotFound();
            }

            return phrase;
        }
        
        // PUT: api/Phrases/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhrase(int id, Phrase phrase)
        {
            if (id != phrase.ID)
            {
                return BadRequest();
            }

            _context.Entry(phrase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhraseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Phrases
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Phrase>> PostPhrase(Phrase phrase)
        {
            _context.Phrases.Add(phrase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhrase", new { id = phrase.ID }, phrase);
        }

        // DELETE: api/Phrases/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Phrase>> DeletePhrase(int id)
        {
            var phrase = await _context.Phrases.FindAsync(id);
            if (phrase == null)
            {
                return NotFound();
            }

            _context.Phrases.Remove(phrase);
            await _context.SaveChangesAsync();

            return phrase;
        }

        private bool PhraseExists(int id)
        {
            return _context.Phrases.Any(e => e.ID == id);
        }
        [HttpGet("SearchbyPhrase")]
        public async Task<IEnumerable<Template>> SearchbyPhrase(string SearchString)
        {

            var phrase = await _context.Phrases.Where(x => x.Text.Contains(SearchString)).ToListAsync();

/*
            var res = _context.Templates.
                                Include(temp => temp.TempPhrases.Where(phr => phr.Text.Contains(SearchString)).ToList());*/


          /*  var res = _context.Phrases.Where(phr => phr.Text.Contains(SearchString))
                                .Include(temp => temp.Template).ToList();
                                
            return res;*/
              
            var result = (from x in phrase
                          join c in _context.Templates
                          on x.Template.ID equals c.ID
                          select c).ToList();
              return result;




        }
    }
}
