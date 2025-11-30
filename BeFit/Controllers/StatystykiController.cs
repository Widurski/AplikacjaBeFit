using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;

namespace BeFit.Controllers
{
    [Authorize]
    public class StatystykiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public StatystykiController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var dataGraniczna = DateTime.Now.AddDays(-28); 

            var statystyki = await _context.CwiczeniaWSesji
                .Include(c => c.SesjaTreningowa)
                .Include(c => c.TypCwiczenia)
                .Where(c => c.SesjaTreningowa != null && c.TypCwiczenia != null
                            && c.SesjaTreningowa.UserId == userId
                            && c.SesjaTreningowa.Data >= dataGraniczna)
                .GroupBy(c => c.TypCwiczenia!.Nazwa)
                .Select(g => new StatystykaViewModel
                {
                    NazwaCwiczenia = g.Key,
                    IloscSerii = g.Count(), 
                    LacznaIloscPowtorzen = g.Sum(x => x.LiczbaPowtorzen),
                    SrednieObciazenie = g.Average(x => x.Obciazenie), 
                    MaksymalneObciazenie = g.Max(x => x.Obciazenie)   
                })
                .ToListAsync();

            return View(statystyki);
        }
    }

    public class StatystykaViewModel
    {
        public string NazwaCwiczenia { get; set; } = string.Empty;
        public int IloscSerii { get; set; }
        public int LacznaIloscPowtorzen { get; set; }
        public double SrednieObciazenie { get; set; }
        public double MaksymalneObciazenie { get; set; }
    }
}