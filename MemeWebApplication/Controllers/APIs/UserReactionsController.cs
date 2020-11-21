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
    public class UserReactionsController : ControllerBase
    {
        private readonly MemeDbContext _context;

        public UserReactionsController(MemeDbContext context)
        {
            _context = context;
        }

        // GET: api/UserReactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReaction>>> GetUserReactions()
        {
            return await _context.UserReactions.ToListAsync();
        }

        // GET: api/UserReactions/5
          [HttpGet("{id}")]
          public async Task<ActionResult<UserReaction>> GetUserReaction(int id,int tempid)
          {
              var userReaction = await _context.UserReactions.FirstOrDefaultAsync(b=>b.UserID==id && b.TemplateID==tempid);
           
              if (userReaction== null)
              {

                var User = new UserReaction
                {
                    UserID = id,
                    TemplateID=tempid

                  };
                return User;
              }
           
              return userReaction;


          }
          //get user Favreactions by user id
           [HttpGet("Fav")]
          public async Task<ActionResult<IEnumerable<Template>>> GetUserfavReaction(int id)
          {
            var userReaction = await _context.UserReactions.Where(m=>m.UserID==id && m.Favourite==true).ToListAsync();

            if (userReaction == null)
            {
                return NotFound();
            }
            var result = (from x in userReaction
                          join z in _context.Templates
                          on x.TemplateID equals z.ID
                          select z).ToList();

            return result;


        }
        //get user Downloadreactions by user id
        [HttpGet("download")]
        public async Task<ActionResult<IEnumerable<Template>>> GetUserDownloadReaction(int id)
        {
            var userReaction = await _context.UserReactions.Where(m => m.UserID == id && m.Download == true).ToListAsync();

            if (userReaction == null)
            {
                return NotFound();
            }

            var result = (from x in userReaction
                          join z in _context.Templates
                          on x.TemplateID equals z.ID
                          select z).ToList();

            return result;


        }
        

        /*    var userReaction = await _context.UserReactions
                .Include(u => u.Template)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (userReaction == null)
            {
                return NotFound();
            }

            return userReaction;
        }*/

        // PUT: api/UserReactions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
       // [HttpPut("{id}")]
   /*    [HttpPut("UpdateUserReaction")]
        public async Task<ActionResult<UserReaction>> PutUserReaction (UserReaction userReaction)
        {
            if (!ModelState.IsValid)
            return BadRequest("Not a valid model");


            if (userReaction.UserID ==null)
            {
                return BadRequest();
            }

            _context.Entry(userReaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return userReaction;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserReactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/
        
        // POST: api/UserReactions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
      /*  [HttpPost("SetReaction")]
      
        public async Task<ActionResult<UserReaction>> PostUserReaction(UserReaction userReaction)
        {
            var x = _context.UserReactions.FirstOrDefaultAsync(c => c.UserID == userReaction.UserID && c.TemplateID == userReaction.TemplateID);
            if(x!=null)
            {
                // _context.Entry(userReaction).State = EntityState.Modified;

               // x.Favourite =false;

                 _context.SaveChangesAsync();
              

            }

            _context.UserReactions.Add(userReaction);
                await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetUserReaction", new { id = userReaction.ID }, userReaction);
        }
       
    */
        // DELETE: api/UserReactions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserReaction>> DeleteUserReaction(int id)
        {
            var userReaction = await _context.UserReactions.FindAsync(id);
            if (userReaction == null)
            {
                return NotFound();
            }

            _context.UserReactions.Remove(userReaction);
            await _context.SaveChangesAsync();

            return userReaction;
        }
       
        private bool UserReactionExists(int id)
        {
            return _context.UserReactions.Any(e => e.ID == id);
        }
        [HttpPut("UnFav")]
        public async Task<ActionResult<UserReaction>> UnFav(UserReaction userReaction)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            //   userReaction.Favourite == false;
            var x =  _context.UserReactions.Where(c => c.UserID == userReaction.UserID && c.TemplateID==userReaction.TemplateID).FirstOrDefault();
            x.Favourite = false;
            await _context.SaveChangesAsync();
             return x ;
         //   return await _context.UserReactions.ToListAsync();

        }
        [HttpPut("UserReaction")]
        public async Task<ActionResult<UserReaction>> UserReaction(UserReaction userReaction)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            //   userReaction.Favourite == false;





            var x = _context.UserReactions.Where(c => c.UserID == userReaction.UserID && c.TemplateID==userReaction.TemplateID).FirstOrDefault();
            var y = _context.UserReactions.Where(c => c.Favourite == false && c.Download == false).FirstOrDefault();
            if(y!=null)
            {
                _context.UserReactions.Remove(y);
                await _context.SaveChangesAsync();

            }


            if (x != null)
            {
                if (x.Favourite == true)
                { x.Favourite = false; }
                else x.Favourite = true;
      
               //  _context.Entry(userReaction).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
           
            else
            {
                userReaction.Favourite = true;
       
                _context.Add(userReaction);
                  await _context.SaveChangesAsync();
                return CreatedAtAction("GetUserReaction", new { id = userReaction.ID }, userReaction);
            }

          
            return x;
            //   return await _context.UserReactions.ToListAsync();

        }
        [HttpPut("UserReactionDownload")]
        public async Task<ActionResult<UserReaction>> UserReactionDownload(UserReaction userReaction)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            //   userReaction.Favourite == false;





            var x = _context.UserReactions.Where(c => c.UserID == userReaction.UserID && c.TemplateID == userReaction.TemplateID).FirstOrDefault();
            var y = _context.UserReactions.Where(c => c.Favourite == false && c.Download == false).FirstOrDefault();
            if (y != null)
            {
                _context.UserReactions.Remove(y);
                await _context.SaveChangesAsync();

            }


            if (x != null)
            {
                if (x.Download == true)
               

                //  _context.Entry(userReaction).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            else
            {
                userReaction.Download = true;

                _context.Add(userReaction);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetUserReaction", new { id = userReaction.ID }, userReaction);
            }


            return x;
            //   return await _context.UserReactions.ToListAsync();

        }

    }
}
