using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangladeshToday.Models;
using System.Text;

namespace BangladeshToday.Controllers
{
    public class UserDetailsController : Controller
    {
        private readonly bangladeshtodayContext _context;

        public UserDetailsController(bangladeshtodayContext context)
        {
            _context = context;
        }

        // GET: UserDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserDetails.ToListAsync());
        }

        // GET: UserDetails/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDetails = await _context.UserDetails
                .SingleOrDefaultAsync(m => m.Username == id);
            if (userDetails == null)
            {
                return NotFound();
            }

            return View(userDetails);
        }

        // GET: UserDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // Sha1 Encryption
        private static string CalculateSha1(string text)
        {
            var enc = Encoding.GetEncoding(65001); // utf-8 code page
            byte[] buffer = enc.GetBytes(text);

            var sha1 = System.Security.Cryptography.SHA1.Create();

            var hash = sha1.ComputeHash(buffer);

            return enc.GetString(hash);
        }

        // POST: UserDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,Email,Role")] UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                userDetails.Password = CalculateSha1(userDetails.Password);
                _context.Add(userDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userDetails);
        }

        // GET: UserDetails/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDetails = await _context.UserDetails.SingleOrDefaultAsync(m => m.Username == id);
            if (userDetails == null)
            {
                return NotFound();
            }
            return View(userDetails);
        }

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,Password,Email,Role")] UserDetails userDetails)
        {
            if (id != userDetails.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDetailsExists(userDetails.Username))
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
            return View(userDetails);
        }

        // GET: UserDetails/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDetails = await _context.UserDetails
                .SingleOrDefaultAsync(m => m.Username == id);
            if (userDetails == null)
            {
                return NotFound();
            }

            return View(userDetails);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userDetails = await _context.UserDetails.SingleOrDefaultAsync(m => m.Username == id);
            _context.UserDetails.Remove(userDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserDetailsExists(string id)
        {
            return _context.UserDetails.Any(e => e.Username == id);
        }
    }
}
