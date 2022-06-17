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
    public class VLettersController : Controller
    {
        private readonly CourseworkContext _context;

        public VLettersController(CourseworkContext context)
        {
            _context = context;
        }

        // GET: VLetters
        public async Task<IActionResult> Index()
        {
            var courseworkContext = _context.VLetters.Include(v => v.CAddressee1).Include(v => v.CAddressee2).Include(v => v.CTopic);
            return View(await courseworkContext.ToListAsync());
        }

        // GET: VLetters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vLetter = await _context.VLetters
                .Include(v => v.CAddressee1)
                .Include(v => v.CAddressee2)
                .Include(v => v.CTopic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vLetter == null)
            {
                return NotFound();
            }

            return View(vLetter);
        }

        // GET: VLetters/Create
        public IActionResult Create()
        {
            ViewData["CAddressee1Id"] = new SelectList(_context.CAddressees, "Id", "AddresseeName");
            ViewData["CAddressee2Id"] = new SelectList(_context.CAddressees, "Id", "AddresseeName");
            ViewData["CTopicId"] = new SelectList(_context.CTopics, "Id", "TopicName");
            return View();
        }

        // POST: VLetters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReceiptTime,DepartureTime,Answer,CTopicId,CAddressee1Id,CAddressee2Id")] VLetter vLetter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vLetter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CAddressee1Id"] = new SelectList(_context.CAddressees, "Id", "AddresseeName", vLetter.CAddressee1Id);
            ViewData["CAddressee2Id"] = new SelectList(_context.CAddressees, "Id", "AddresseeName", vLetter.CAddressee2Id);
            ViewData["CTopicId"] = new SelectList(_context.CTopics, "Id", "TopicName", vLetter.CTopicId);
            return View(vLetter);
        }

        // GET: VLetters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vLetter = await _context.VLetters.FindAsync(id);
            if (vLetter == null)
            {
                return NotFound();
            }
            ViewData["CAddressee1Id"] = new SelectList(_context.CAddressees, "Id", "AddresseeName", vLetter.CAddressee1Id);
            ViewData["CAddressee2Id"] = new SelectList(_context.CAddressees, "Id", "AddresseeName", vLetter.CAddressee2Id);
            ViewData["CTopicId"] = new SelectList(_context.CTopics, "Id", "TopicName", vLetter.CTopicId);
            return View(vLetter);
        }

        // POST: VLetters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReceiptTime,DepartureTime,Answer,CTopicId,CAddressee1Id,CAddressee2Id")] VLetter vLetter)
        {
            if (id != vLetter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vLetter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VLetterExists(vLetter.Id))
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
            ViewData["CAddressee1Id"] = new SelectList(_context.CAddressees, "Id", "AddresseeName", vLetter.CAddressee1Id);
            ViewData["CAddressee2Id"] = new SelectList(_context.CAddressees, "Id", "AddresseeName", vLetter.CAddressee2Id);
            ViewData["CTopicId"] = new SelectList(_context.CTopics, "Id", "TopicName", vLetter.CTopicId);
            return View(vLetter);
        }

        // GET: VLetters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vLetter = await _context.VLetters
                .Include(v => v.CAddressee1)
                .Include(v => v.CAddressee2)
                .Include(v => v.CTopic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vLetter == null)
            {
                return NotFound();
            }

            return View(vLetter);
        }

        // POST: VLetters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vLetter = await _context.VLetters.FindAsync(id);
            _context.VLetters.Remove(vLetter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VLetterExists(int id)
        {
            return _context.VLetters.Any(e => e.Id == id);
        }
    }
}
