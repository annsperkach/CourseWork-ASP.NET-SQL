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
    public class CAddresseesController : Controller
    {
        private readonly CourseworkContext _context;

        public CAddresseesController(CourseworkContext context)
        {
            _context = context;
        }

        // GET: CAddressees
        public async Task<IActionResult> Index()
        {
            return View(await _context.CAddressees.ToListAsync());
        }

        // GET: CAddressees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cAddressee = await _context.CAddressees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cAddressee == null)
            {
                return NotFound();
            }

            return View(cAddressee);
        }

        // GET: CAddressees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CAddressees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Passport,AddresseeName")] CAddressee cAddressee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cAddressee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cAddressee);
        }

        // GET: CAddressees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cAddressee = await _context.CAddressees.FindAsync(id);
            if (cAddressee == null)
            {
                return NotFound();
            }
            return View(cAddressee);
        }

        // POST: CAddressees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Passport,AddresseeName")] CAddressee cAddressee)
        {
            if (id != cAddressee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cAddressee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CAddresseeExists(cAddressee.Id))
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
            return View(cAddressee);
        }

        // GET: CAddressees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cAddressee = await _context.CAddressees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cAddressee == null)
            {
                return NotFound();
            }

            return View(cAddressee);
        }

        // POST: CAddressees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cAddressee = await _context.CAddressees.FindAsync(id);
            _context.CAddressees.Remove(cAddressee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CAddresseeExists(int id)
        {
            return _context.CAddressees.Any(e => e.Id == id);
        }
    }
}
