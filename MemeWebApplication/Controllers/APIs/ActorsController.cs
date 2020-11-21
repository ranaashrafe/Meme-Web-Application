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
    public class ActorsController : ControllerBase
    {
        private readonly MemeDbContext _context;

        public ActorsController(MemeDbContext context)
        {
            _context = context;
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActors()
        {
            return await _context.Actors.ToListAsync();
        }

        // GET: api/Actors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Actor>> GetActor(int id)
        {
            var actor = await _context.Actors.FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, Actor actor)
        {
            if (id != actor.ID)
            {
                return BadRequest();
            }

            _context.Entry(actor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
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

        // POST: api/Actors
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor(Actor actor)
        {
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActor", new { id = actor.ID }, actor);
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Actor>> DeleteActor(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();

            return actor;
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.ID == id);
        }
        [HttpGet("SearchbyActor")]
        public async Task<ActionResult<IEnumerable<Template>>> SearchbyActor(string SearchString)
        {

             var actors = await _context.Actors.Where(x => x.Name.Contains(SearchString)).ToListAsync();
            /*var Result = await _context.Templates
               .Include(temp => temp.ActorTemp)
               .Include(act => act.  .FirstOrDefault(obj => obj.);*/

          /*  var Result = _context.Templates
                .Include(temp => temp.ActorTemp.FirstOrDefault(actTemp => actTemp.TemplateID == temp.ID))
                .ThenInclude(act=>act.ActorID)
                ;*/


            var result = (from x in actors
                          join z in _context.ActorTemplates
                          on x.ID equals z.ActorID
                          join d in _context.Templates
                         on z.TemplateID equals d.ID
                          select d).ToList();
            return result;
        }

    }
}







/*

              var Actor= await _context.Templates.Where(x => x.Image ==result.ToString()).ToListAsync();

              return Actor;
        
    }
}
*/ 