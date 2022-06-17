using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CTopicsController : Controller
    {
        private readonly CourseworkContext _context;

        public CTopicsController(CourseworkContext context)
        {
            _context = context;
        }

        // GET: CTopics
        public async Task<IActionResult> Index()
        {
            return View(await _context.CTopics.ToListAsync());
        }

        // GET: CTopics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cTopic = await _context.CTopics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cTopic == null)
            {
                return NotFound();
            }

            return View(cTopic);
        }

        // GET: CTopics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CTopics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TopicName")] CTopic cTopic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cTopic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cTopic);
        }

        // GET: CTopics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cTopic = await _context.CTopics.FindAsync(id);
            if (cTopic == null)
            {
                return NotFound();
            }
            return View(cTopic);
        }

        // POST: CTopics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TopicName")] CTopic cTopic)
        {
            if (id != cTopic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cTopic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CTopicExists(cTopic.Id))
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
            return View(cTopic);
        }

        // GET: CTopics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cTopic = await _context.CTopics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cTopic == null)
            {
                return NotFound();
            }

            return View(cTopic);
        }

        // POST: CTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cTopic = await _context.CTopics.FindAsync(id);
            _context.CTopics.Remove(cTopic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CTopicExists(int id)
        {
            return _context.CTopics.Any(e => e.Id == id);
        }
    }
}
