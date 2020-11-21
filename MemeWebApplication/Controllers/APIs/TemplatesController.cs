using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemeWebApplication.AppDbContext;
using MemeWebApplication.Models;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Data;

namespace MemeWebApplication.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly MemeDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public TemplatesController(MemeDbContext context, IHostingEnvironment env)
        {
            _context = context;
            hostingEnvironment = env;

        }

        // GET: api/Templates
      [HttpGet("Template")]
      
        public async Task<ActionResult<IEnumerable<Template>>> GetTemplates()
        {

            var result = await _context.Templates.ToListAsync();
            result.Reverse();
        
            return result;
           
        }
        
        // GET: api/Templates/5
        [HttpGet("{id}")]
        
        public async Task<ActionResult<Template>> GetTemplate(int id)
        {
            var template = await _context.Templates.FindAsync(id);

            if (template == null)
            {
                return NotFound();
            }

            return template;
        }

       

        // PUT: api/Templates/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemplate(int id, Template template)
        {
            if (id != template.ID)
            {
                return BadRequest();
            }

            _context.Entry(template).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemplateExists(id))
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

        // POST: api/Templates
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
      

        [HttpPost("AddTemplate")]
        public async Task<ActionResult<Template>> PostTemplate(Template template)
        {
         //   byte[] byt = System.Text.Encoding.UTF8.GetBytes(FilmName);
           // FilmName = Convert.ToBase64String(byt);
            var temp = new Template
            {
               
                FilmName =template.FilmName,
                Image=template.Image



            };

            _context.Templates.Add(template);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTemplate", new { id = template.ID }, template);
        }

        // DELETE: api/Templates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Template>> DeleteTemplate(int id)
        {
            var template = await _context.Templates.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }

            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();

            return template;
        }

        private bool TemplateExists(int id)
        {
            return _context.Templates.Any(e => e.ID == id);
        }
      
        // GET: api/Templates/5

         [HttpGet("SearchbyFilm")]
     //   [Route("api/SearchbyFilm/{SearchString}")]

        public async Task<ActionResult<IEnumerable<Template>>> SearchbyFilm(string SearchString)
          {
              var template = await _context.Templates.Where(x=>x.FilmName.Contains(SearchString)).ToListAsync();
             
            if (template == null)
            
              {
                  return NotFound();
              }

              return template;
          }
        [HttpPost("UploadImage")]
        public async Task<string> UploadImage(IFormFile file)
        {
            string output = "";
            try
            {
                List<string> types = new List<string>();
                if (ModelState.IsValid)
                {
                    // Code to upload image if not null
                    if (file != null && file.Length != 0)
                    {
                        // Create a File Info
                        FileInfo fi = new FileInfo(file.FileName);

                        // This code creates a unique file name to prevent duplications
                        // stored at the file location
                        var newFilename = String.Format("{0:d}",
                                              (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                        var webPath = hostingEnvironment.WebRootPath;
                        var path = Path.Combine("", webPath + @"\uploads\" + newFilename);

                        // IMPORTANT: The pathToSave variable will be save on the column in the database
                        var pathToSave = @"/uploads/" + newFilename;

                        // This stream the physical file to the allocate wwwroot/ImageFiles folder
                        using (var stream = new FileStream(path, FileMode.CreateNew))
                        {
                            await file.CopyToAsync(stream);

                        }

                        // This save the path to the record
                        output = pathToSave;

                    }
                }

            }
            catch (Exception ex)
            {
                output = ex.Message;
            }
            return output;
        }

    } 
}
