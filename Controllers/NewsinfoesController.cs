﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangladeshToday.Models;

namespace BangladeshToday.Controllers
{
    public class NewsinfoesController : Controller
    {
        private readonly bangladeshtodayContext _context;

        public NewsinfoesController(bangladeshtodayContext context)
        {
            _context = context;
        }

        // GET: Newsinfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Newsinfo.ToListAsync());
        }

        // GET: Newsinfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsinfo = await _context.Newsinfo
                .SingleOrDefaultAsync(m => m.Newsserial == id);
            if (newsinfo == null)
            {
                return NotFound();
            }

            return View(newsinfo);
        }

        // GET: Newsinfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Newsinfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Newsserial,Title,Category,Description,Author,Datetime,Keyword")] Newsinfo newsinfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newsinfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsinfo);
        }

        // GET: Newsinfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsinfo = await _context.Newsinfo.SingleOrDefaultAsync(m => m.Newsserial == id);
            if (newsinfo == null)
            {
                return NotFound();
            }
            return View(newsinfo);
        }

        // POST: Newsinfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Newsserial,Title,Category,Description,Author,Datetime,Keyword")] Newsinfo newsinfo)
        {
            if (id != newsinfo.Newsserial)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsinfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsinfoExists(newsinfo.Newsserial))
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
            return View(newsinfo);
        }

        // GET: Newsinfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsinfo = await _context.Newsinfo
                .SingleOrDefaultAsync(m => m.Newsserial == id);
            if (newsinfo == null)
            {
                return NotFound();
            }

            return View(newsinfo);
        }

        // POST: Newsinfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsinfo = await _context.Newsinfo.SingleOrDefaultAsync(m => m.Newsserial == id);
            _context.Newsinfo.Remove(newsinfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsinfoExists(int id)
        {
            return _context.Newsinfo.Any(e => e.Newsserial == id);
        }
    }
}