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
                .Where(c => c.SesjaTreningowa.UserId == userId)
                .Where(c => c.SesjaTreningowa.Data >= dataGraniczna)
                .GroupBy(c => c.TypCwiczenia.Nazwa)
                .Select(g => new StatystykaViewModel
                {
                    NazwaCwiczenia = g.Key,
                    IloscWykonan = g.Count(),
                    SumaCiezaru = g.Sum(x => x.Obciazenie * x.LiczbaPowtorzen)
                })
                .ToListAsync();

            return View(statystyki);
        }
    }

    public class StatystykaViewModel
    {
        public string NazwaCwiczenia { get; set; }
        public int IloscWykonan { get; set; }
        public double SumaCiezaru { get; set; }
    }
}