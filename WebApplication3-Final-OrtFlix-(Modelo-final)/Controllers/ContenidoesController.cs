using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3_Final_OrtFlix__Modelo_final_.Context;
using WebApplication3_Final_OrtFlix__Modelo_final_.Models;

namespace WebApplication3_Final_OrtFlix__Modelo_final_.Controllers
{
    public class ContenidoesController : Controller
    {
        private readonly OrtflixDatabaseContext _context;

        public ContenidoesController(OrtflixDatabaseContext context)
        {
            _context = context;
        }

        // GET: Contenidoes
        public async Task<IActionResult> Index()
        {
              return _context.Contenidos != null ? 
                          View(await _context.Contenidos.ToListAsync()) :
                          Problem("Entity set 'OrtflixDatabaseContext.Contenidos'  is null.");
        }

        // GET: Contenidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contenidos == null)
            {
                return NotFound();
            }

            var contenido = await _context.Contenidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contenido == null)
            {
                return NotFound();
            }

            return View(contenido);
        }

        // GET: Contenidoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contenidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Premium,TipoContenido,Valoracion,Duracion,Clasificacion")] Contenido contenido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contenido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contenido);
        }

        // GET: Contenidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contenidos == null)
            {
                return NotFound();
            }

            var contenido = await _context.Contenidos.FindAsync(id);
            if (contenido == null)
            {
                return NotFound();
            }
            return View(contenido);
        }

        // POST: Contenidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Premium,TipoContenido,Valoracion,Duracion,Clasificacion")] Contenido contenido)
        {
            if (id != contenido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contenido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContenidoExists(contenido.Id))
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
            return View(contenido);
        }

        // GET: Contenidoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contenidos == null)
            {
                return NotFound();
            }

            var contenido = await _context.Contenidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contenido == null)
            {
                return NotFound();
            }

            return View(contenido);
        }

        // POST: Contenidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contenidos == null)
            {
                return Problem("Entity set 'OrtflixDatabaseContext.Contenidos'  is null.");
            }
            var contenido = await _context.Contenidos.FindAsync(id);
            if (contenido != null)
            {
                _context.Contenidos.Remove(contenido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContenidoExists(int id)
        {
          return (_context.Contenidos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
