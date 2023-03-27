using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplicacionDiez.Models;

namespace AplicacionDiez.Controllers
{
    public class SucursalesController : Controller
    {
        private readonly ACMEContext _context;

        public SucursalesController(ACMEContext context)
        {
            _context = context;
        }

        // GET: Sucursales
        public async Task<IActionResult> Index()
        {
              return _context.Sucursales != null ? 
                          View(await _context.Sucursales.ToListAsync()) :
                          Problem("Entity set 'ACMEContext.Sucursales'  is null.");
        }

        // GET: Sucursales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sucursales == null)
            {
                return NotFound();
            }

            var sucursales = await _context.Sucursales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sucursales == null)
            {
                return NotFound();
            }

            return View(sucursales);
        }

        // GET: Sucursales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sucursales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Ubicacion")] Sucursales sucursales)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sucursales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sucursales);
        }

        // GET: Sucursales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sucursales == null)
            {
                return NotFound();
            }

            var sucursales = await _context.Sucursales.FindAsync(id);
            if (sucursales == null)
            {
                return NotFound();
            }
            return View(sucursales);
        }

        // POST: Sucursales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Ubicacion")] Sucursales sucursales)
        {
            if (id != sucursales.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sucursales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SucursalesExists(sucursales.Id))
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
            return View(sucursales);
        }

        // GET: Sucursales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sucursales == null)
            {
                return NotFound();
            }

            var sucursales = await _context.Sucursales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sucursales == null)
            {
                return NotFound();
            }

            return View(sucursales);
        }

        // POST: Sucursales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sucursales == null)
            {
                return Problem("Entity set 'ACMEContext.Sucursales'  is null.");
            }
            var sucursales = await _context.Sucursales.FindAsync(id);
            if (sucursales != null)
            {
                _context.Sucursales.Remove(sucursales);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SucursalesExists(int id)
        {
          return (_context.Sucursales?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
