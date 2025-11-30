using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BeFit.Controllers
{
    [Authorize]
    public class SesjeTreningoweController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SesjeTreningoweController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var sesje = await _context.SesjeTreningowe
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.Data)
                .ToListAsync();
            return View(sesje);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var userId = _userManager.GetUserId(User);
#pragma warning disable CS8620 
            var sesja = await _context.SesjeTreningowe
                .Include(s => s.User)
                .Include(s => s.Cwiczenia)
                .ThenInclude(c => c.TypCwiczenia)
                .FirstOrDefaultAsync(m => m.Id == id);
#pragma warning restore CS8620 

            if (sesja == null || sesja.UserId != userId) return NotFound();

            return View(sesja);
        }


        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,Komentarz")] SesjaTreningowa sesja)
        {
            var userId = _userManager.GetUserId(User);
            sesja.UserId = userId;

            
            ModelState.Remove("UserId");
            ModelState.Remove("User");
            ModelState.Remove("Cwiczenia");

            if (ModelState.IsValid)
            {
                _context.Add(sesja);
                await _context.SaveChangesAsync();
                
                
                return RedirectToAction(nameof(Details), new { id = sesja.Id }); 
            }
            return View(sesja);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var userId = _userManager.GetUserId(User);
            var sesja = await _context.SesjeTreningowe.FindAsync(id);
            if (sesja == null || sesja.UserId != userId) return NotFound();
            return View(sesja);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SesjaTreningowa sesja)
        {
            if (id != sesja.Id) return NotFound();
            var original = await _context.SesjeTreningowe.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (original == null) return NotFound();
            
            sesja.UserId = original.UserId;
            ModelState.Remove("UserId");
            ModelState.Remove("User");
            ModelState.Remove("Cwiczenia");

            if (ModelState.IsValid)
            {
                _context.Update(sesja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sesja);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var userId = _userManager.GetUserId(User);
            var sesja = await _context.SesjeTreningowe.FirstOrDefaultAsync(m => m.Id == id);
            if (sesja == null || sesja.UserId != userId) return NotFound();
            return View(sesja);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sesja = await _context.SesjeTreningowe.FindAsync(id);
            if (sesja != null) _context.SesjeTreningowe.Remove(sesja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}