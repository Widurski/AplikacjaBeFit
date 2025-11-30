using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Authorization; 

namespace BeFit.Controllers
{
    
    [Authorize(Roles = "Admin")]
    public class TypyCwiczenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypyCwiczenController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [AllowAnonymous] 
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypyCwiczen.ToListAsync());
        }

        
        [AllowAnonymous] 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var typCwiczenia = await _context.TypyCwiczen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typCwiczenia == null) return NotFound();

            return View(typCwiczenia);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Opis")] TypCwiczenia typCwiczenia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typCwiczenia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typCwiczenia);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var typCwiczenia = await _context.TypyCwiczen.FindAsync(id);
            if (typCwiczenia == null) return NotFound();
            return View(typCwiczenia);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Opis")] TypCwiczenia typCwiczenia)
        {
            if (id != typCwiczenia.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typCwiczenia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypCwiczeniaExists(typCwiczenia.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(typCwiczenia);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var typCwiczenia = await _context.TypyCwiczen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typCwiczenia == null) return NotFound();

            return View(typCwiczenia);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typCwiczenia = await _context.TypyCwiczen.FindAsync(id);
            if (typCwiczenia != null)
            {
                _context.TypyCwiczen.Remove(typCwiczenia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypCwiczeniaExists(int id)
        {
            return _context.TypyCwiczen.Any(e => e.Id == id);
        }
    }
}