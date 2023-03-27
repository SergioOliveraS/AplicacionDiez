using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplicacionDiez.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace AplicacionDiez.Controllers
{
    public class CantidadSucursalsController : Controller
    {
        private readonly ACMEContext _context;

        public CantidadSucursalsController(ACMEContext context)
        {
            _context = context;
        }

        // GET: CantidadSucursals
        public async Task<IActionResult> Index()
        {
            var aCMEContext = _context.CantidadSucursales.Include(c => c.Producto).Include(c => c.Sucursal);
            return View(await aCMEContext.ToListAsync());
        }

        // GET: CantidadSucursals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CantidadSucursales == null)
            {
                return NotFound();
            }

            var cantidadSucursal = await _context.CantidadSucursales
                .Include(c => c.Producto)
                .Include(c => c.Sucursal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cantidadSucursal == null)
            {
                return NotFound();
            }

            return View(cantidadSucursal);
        }

        // GET: CantidadSucursals/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre");
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Nombre");
            return View();
        }

        // POST: CantidadSucursals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SucursalId,ProductoId,CantidadProducto")] CantidadSucursal cantidadSucursal)
        {
            bool valido = false;
            var aCMEContext = _context.CantidadSucursales.Include(c => c.Producto).Include(c => c.Sucursal);
            if (ModelState.IsValid)
            {
                var lista = aCMEContext.ToList();
                foreach (var c in lista)
                {
                    if (cantidadSucursal.ProductoId == c.ProductoId && cantidadSucursal.SucursalId == c.SucursalId)
                    {
                        c.CantidadProducto += cantidadSucursal.CantidadProducto;
                        valido = true;
                        break;
                    }
                }
                if(valido == false)
                {
                    _context.Add(cantidadSucursal);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", cantidadSucursal.ProductoId);
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Nombre", cantidadSucursal.SucursalId);
            return View(cantidadSucursal);
        }

        // GET: CantidadSucursals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CantidadSucursales == null)
            {
                return NotFound();
            }

            var cantidadSucursal = await _context.CantidadSucursales.FindAsync(id);
            if (cantidadSucursal == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", cantidadSucursal.ProductoId);
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Nombre", cantidadSucursal.SucursalId);
            return View(cantidadSucursal);
        }

        // POST: CantidadSucursals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SucursalId,ProductoId,CantidadProducto")] CantidadSucursal cantidadSucursal)
        {
            if (id != cantidadSucursal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cantidadSucursal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CantidadSucursalExists(cantidadSucursal.Id))
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
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", cantidadSucursal.ProductoId);
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Nombre", cantidadSucursal.SucursalId);
            return View(cantidadSucursal);
        }

        // GET: CantidadSucursals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CantidadSucursales == null)
            {
                return NotFound();
            }

            var cantidadSucursal = await _context.CantidadSucursales
                .Include(c => c.Producto)
                .Include(c => c.Sucursal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cantidadSucursal == null)
            {
                return NotFound();
            }

            return View(cantidadSucursal);
        }

        // POST: CantidadSucursals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CantidadSucursales == null)
            {
                return Problem("Entity set 'ACMEContext.CantidadSucursales'  is null.");
            }
            var cantidadSucursal = await _context.CantidadSucursales.FindAsync(id);
            if (cantidadSucursal != null)
            {
                _context.CantidadSucursales.Remove(cantidadSucursal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CantidadSucursalExists(int id)
        {
          return (_context.CantidadSucursales?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
