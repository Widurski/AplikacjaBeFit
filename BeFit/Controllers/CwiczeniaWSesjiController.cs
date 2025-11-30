using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BeFit.Controllers
{
    [Authorize]
    public class CwiczeniaWSesjiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CwiczeniaWSesjiController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        
        
        public IActionResult Create(int? sesjaId) 
        {
            var userId = _userManager.GetUserId(User);

            var userSessions = _context.SesjeTreningowe.Where(s => s.UserId == userId);

            
            ViewData["SesjaTreningowaId"] = new SelectList(userSessions, "Id", "Data", sesjaId);
            
            ViewData["TypCwiczeniaId"] = new SelectList(_context.TypyCwiczen, "Id", "Nazwa");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SesjaTreningowaId,TypCwiczeniaId,LiczbaPowtorzen,Obciazenie,Tempo,ZakresTetna")] CwiczenieWSesji cwiczenie)
        {
            var userId = _userManager.GetUserId(User);
            
            
            var sesjaOk = _context.SesjeTreningowe.Any(s => s.Id == cwiczenie.SesjaTreningowaId && s.UserId == userId);
            if (!sesjaOk) return Forbid();

            if (ModelState.IsValid)
            {
                _context.Add(cwiczenie);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Details", "SesjeTreningowe", new { id = cwiczenie.SesjaTreningowaId });
            }
            
            ViewData["SesjaTreningowaId"] = new SelectList(_context.SesjeTreningowe.Where(s => s.UserId == userId), "Id", "Data", cwiczenie.SesjaTreningowaId);
            ViewData["TypCwiczeniaId"] = new SelectList(_context.TypyCwiczen, "Id", "Nazwa", cwiczenie.TypCwiczeniaId);
            return View(cwiczenie);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var cwiczenie = await _context.CwiczeniaWSesji
                .Include(c => c.SesjaTreningowa)
                .Include(c => c.TypCwiczenia)
                .FirstOrDefaultAsync(m => m.Id == id);

            var userId = _userManager.GetUserId(User);
#pragma warning disable CS8602 
            if (cwiczenie == null || cwiczenie.SesjaTreningowa.UserId != userId) return NotFound();
#pragma warning restore CS8602 

            return View(cwiczenie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cwiczenie = await _context.CwiczeniaWSesji.Include(c => c.SesjaTreningowa).FirstOrDefaultAsync(c => c.Id == id);
            int? sesjaId = cwiczenie?.SesjaTreningowaId;

            if (cwiczenie != null)
            {
                _context.CwiczeniaWSesji.Remove(cwiczenie);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Details", "SesjeTreningowe", new { id = sesjaId });
        }
    }
}