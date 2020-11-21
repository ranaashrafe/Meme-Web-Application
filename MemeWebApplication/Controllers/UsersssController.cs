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

namespace MemeWebApplication.Controllers
{
    public class UsersssController : Controller
    {
        private readonly MemeDbContext _context;

        public UsersssController(MemeDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
          
        }
   
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserName,Email,Password,PhoneNumber,Coins,IsBlocked")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
      
       
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserName,Email,Password,PhoneNumber,Coins,IsBlocked")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
        // GET: Users/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("ID,UserName,Email,Password,PhoneNumber")] User user)
        {
            if (ModelState.IsValid)
            {
                var isExit = IsEmailExit(user.Email);
                if (isExit)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);

                }

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        [NonAction]

        public bool IsEmailExit(string email)
        {
            var user =  _context.Users.Where(a => a.Email == email).FirstOrDefault();
            return  user != null; 


        }
        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user,string ReturnURL="")
        {
            var usr = _context.Users.Where(a => a.Email == user.Email && a.Password == user.Password).FirstOrDefaultAsync();
            if(user.IsBlocked==false)
            {
                string message = "";
                var v = _context.Users.Where(a => a.Email == user.Email && a.Password==user.Password).FirstOrDefaultAsync();
                if(v !=null)
                 {
                    HttpContext.Session.SetString("Email", user.Email);

                    if (user.ID == 1)
                    {
                        return RedirectToAction("LoggedInAdmin");
                    }
                    return RedirectToAction("Index","Templates");
                }
                
            }
            else
            {
                ModelState.AddModelError("Email", "this user is blocked.");
            }
            return View();

        }
        //Filteration Function
        public ActionResult Filteration(string FilmName,string ActorName)
        {
            


             var result = (from c in _context.Templates
                         select c).ToList();
            if (!string.IsNullOrEmpty(FilmName))
            {
                result = (_context.Templates.Where(x => x.FilmName.Contains(FilmName))).ToList();

            }

             var result2 = (from b in _context.Actors
                          select b).ToList();
              if(!string.IsNullOrEmpty(ActorName))
              {
                  result2 = (_context.Actors.Where(x => x.Name.Contains(ActorName))).ToList();
              }
            return View((result.Cast<Template>().Concat(result2.Cast<Template>())));
               
           
        }




    }
}
