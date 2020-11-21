using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemeWebApplication.AppDbContext;
using MemeWebApplication.Models;

namespace MemeWebApplication.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorTemplatesController : ControllerBase
    {
        private readonly MemeDbContext _context;

        public ActorTemplatesController(MemeDbContext context)
        {
            _context = context;
        }

        // GET: api/ActorTemplates
               [HttpGet]
              public async Task<ActionResult<IEnumerable<ActorTemplates>>> GetActorTemplates()
               {
                   return await _context.ActorTemplates.ToListAsync();
               }

               // GET: api/ActorTemplates/5
               [HttpGet("{id}")]
               public async Task<ActionResult<ActorTemplates>> GetActorTemplates(int id)
               {
                   var actorTemplates = await _context.ActorTemplates.FindAsync(id);

                   if (actorTemplates == null)
                   {
                       return NotFound();
                   }

                   return actorTemplates;
               }

               // PUT: api/ActorTemplates/5
               // To protect from overposting attacks, please enable the specific properties you want to bind to, for
               // more details see https://aka.ms/RazorPagesCRUD.
              [HttpPut("{id}")]
               public async Task<IActionResult> PutActorTemplates(int id, ActorTemplates actorTemplates)
               {
                   if (id != actorTemplates.ID)
                   {
                       return BadRequest();
                   }

                   _context.Entry(actorTemplates).State = EntityState.Modified;

                   try
                   {
                       await _context.SaveChangesAsync();
                   }
                   catch (DbUpdateConcurrencyException)
                   {
                       if (!ActorTemplatesExists(id))
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

               // POST: api/ActorTemplates
               // To protect from overposting attacks, please enable the specific properties you want to bind to, for
               // more details see https://aka.ms/RazorPagesCRUD.
               [HttpPost]
               public async Task<ActionResult<ActorTemplates>> PostActorTemplates(ActorTemplates actorTemplates)
               {
                   _context.ActorTemplates.Add(actorTemplates);
                   await _context.SaveChangesAsync();

                   return CreatedAtAction("GetActorTemplates", new { id = actorTemplates.ID }, actorTemplates);
               }

               // DELETE: api/ActorTemplates/5
               [HttpDelete("{id}")]
               public async Task<ActionResult<ActorTemplates>> DeleteActorTemplates(int id)
               {
                   var actorTemplates = await _context.ActorTemplates.FindAsync(id);
                   if (actorTemplates == null)
                   {
                       return NotFound();
                   }

                   _context.ActorTemplates.Remove(actorTemplates);
                   await _context.SaveChangesAsync();

                   return actorTemplates;
               }

               private bool ActorTemplatesExists(int id)
               {
                   return _context.ActorTemplates.Any(e => e.ID == id);
               }
     /*   [HttpGet("SearchbyActor")]
        //   [Route("api/SearchbyFilm/{SearchString}")]

        public async Task<ActionResult<IEnumerable<Actor>>> SearchbyActor(string SearchString)
        {
            var template = await _context.Actors.Where(x => x.Name.Contains(SearchString)).ToListAsync();

            if (template == null)

            {
                return NotFound();
            }

            return template;
        }*/
    }
}
