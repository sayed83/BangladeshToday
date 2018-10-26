using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BangladeshToday.Models;

namespace BangladeshToday.Controllers
{
    [Produces("application/json")]
    [Route("api/NewsinfoesApi")]
    public class NewsinfoesApiController : Controller
    {
        private readonly bangladeshtodayContext _context;

        public NewsinfoesApiController(bangladeshtodayContext context)
        {
            _context = context;
        }

        // GET: api/NewsinfoesApi
        [HttpGet]
        public IEnumerable<Newsinfo> GetNewsinfo()
        {
            return _context.Newsinfo;
        }

        // GET: api/NewsinfoesApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsinfo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newsinfo = await _context.Newsinfo.SingleOrDefaultAsync(m => m.Newsserial == id);

            if (newsinfo == null)
            {
                return NotFound();
            }

            return Ok(newsinfo);
        }

        // PUT: api/NewsinfoesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsinfo([FromRoute] int id, [FromBody] Newsinfo newsinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != newsinfo.Newsserial)
            {
                return BadRequest();
            }

            _context.Entry(newsinfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsinfoExists(id))
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

        // POST: api/NewsinfoesApi
        [HttpPost]
        public async Task<IActionResult> PostNewsinfo(Newsinfo newsinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Newsinfo.Add(newsinfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNewsinfo", new { id = newsinfo.Newsserial }, newsinfo);
        }

        // DELETE: api/NewsinfoesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsinfo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newsinfo = await _context.Newsinfo.SingleOrDefaultAsync(m => m.Newsserial == id);
            if (newsinfo == null)
            {
                return NotFound();
            }

            _context.Newsinfo.Remove(newsinfo);
            await _context.SaveChangesAsync();

            return Ok(newsinfo);
        }

        private bool NewsinfoExists(int id)
        {
            return _context.Newsinfo.Any(e => e.Newsserial == id);
        }
    }
}