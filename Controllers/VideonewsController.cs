using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangladeshToday.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BangladeshToday.Controllers
{
    public class VideonewsController : Controller
    {
        private readonly bangladeshtodayContext _context;

        public VideonewsController(bangladeshtodayContext context)
        {
            _context = context;
        }

        // GET: Videonews
        public async Task<IActionResult> Index()
        {
            return View(await _context.Videonews.ToListAsync());
        }

        // GET: Videonews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videonews = await _context.Videonews
                .SingleOrDefaultAsync(m => m.VideoNewsId == id);
            if (videonews == null)
            {
                return NotFound();
            }

            return View(videonews);
        }

        // GET: Videonews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Videonews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VideoNewsId,Title,Category,Keyword,Datetime,Path")] Videonews videonews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videonews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(videonews);
        }

        // GET: Videonews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videonews = await _context.Videonews.SingleOrDefaultAsync(m => m.VideoNewsId == id);
            if (videonews == null)
            {
                return NotFound();
            }
            return View(videonews);
        }

        // POST: Videonews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VideoNewsId,Title,Category,Keyword,Datetime,Path")] Videonews videonews)
        {
            if (id != videonews.VideoNewsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videonews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideonewsExists(videonews.VideoNewsId))
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
            return View(videonews);
        }

        // GET: Videonews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videonews = await _context.Videonews
                .SingleOrDefaultAsync(m => m.VideoNewsId == id);
            if (videonews == null)
            {
                return NotFound();
            }

            return View(videonews);
        }

        // POST: Videonews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videonews = await _context.Videonews.SingleOrDefaultAsync(m => m.VideoNewsId == id);
            _context.Videonews.Remove(videonews);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideonewsExists(int id)
        {
            return _context.Videonews.Any(e => e.VideoNewsId == id);
        }


        // UploadFile Get
        public ActionResult UploadFile(int Vnewsserial)
        {

            List<Allvideo> content = (from p in _context.Allvideo
                                          where p.VideoId == Vnewsserial
                                      orderby p.VideoSerial
                                          select p).ToList();
            ViewData["pictures"] = content;
            string path = (from s in _context.Videonews where s.VideoNewsId == Vnewsserial select s.Path).FirstOrDefault();
            ViewData["apath"] = path;


            return View(_context.Videonews.Find(Vnewsserial));
        }

        // UploadFile Post
        [HttpPost, ActionName("UploadFile")]
        public ActionResult UploadFile(IFormFile file, int id)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return Content("file not selected");
                if (file.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "itemimages/" + file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    Videonews st = (from s in _context.Videonews where s.VideoNewsId == id select s).First();
                    st.Path = file.FileName;
                    _context.SaveChanges();

                    int psl = (from ip in _context.Allvideo where ip.VideoId == id orderby ip.VideoSerial descending select ip.VideoId).FirstOrDefault();
                    psl++;

                    Allvideo ipc = new Allvideo();
                    ipc.VideoId = id;
                    ipc.VideoSerial = psl.ToString();
                    ipc.VideoPath = file.FileName;
                    _context.Add(ipc);
                    _context.SaveChanges();
                }
                ViewBag.Message = "File Uploaded Successfully!!" + ":" + id;
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }


       //Picture Activation
       [HttpPost]
        public string ActivatePicture(int id, string path)
        {
            string a = "1";
            Videonews st = (from s in _context.Videonews where s.VideoNewsId == id select s).First();
            st.Path = path;
            _context.SaveChanges();
            return a;

        }
    }
}
