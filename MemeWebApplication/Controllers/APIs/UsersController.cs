using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemeWebApplication.AppDbContext;
using MemeWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace MemeWebApplication.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MemeDbContext _context;

        public UsersController(MemeDbContext context)
        {
            _context = context;
        }

      // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateUserInfo")]
        public async Task<ActionResult<User>> PutUser(User user)
        {

            if (!ModelState.IsValid)
            {

                return BadRequest();
            }
         


            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return user;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.ID))
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

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
   
        public async Task<ActionResult<User>> PostUser(User user)
        {

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.ID }, user);
        }
        
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);

        }
      
       [HttpPost("Register")]
    //    [Route("Register")]
        public async Task<ActionResult<User>> Register([FromBody]User userDto)
        {

            if (!ModelState.IsValid)
            {

                return BadRequest();
            }
            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password=userDto.Password,
                
                PhoneNumber = userDto.PhoneNumber


            };
            try
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;

            }
            catch
            { 

                return BadRequest();
            }
        }
        [HttpPost("Login")]

        public async Task<ActionResult<User>> Login(User _user)
        {
            
            var user = await _context.Users.FirstOrDefaultAsync(a => a.Email == _user.Email && a.Password == _user.Password);
            //    Models.Login login = new Login { User = user };


            if (!ModelState.IsValid)
            {

                return BadRequest();
            }


            else if (user != null)
            {

                if (user.IsBlocked)
                {
                    return NotFound();

                }
                return user;

            }
            return NotFound();
            }

        



    }
}