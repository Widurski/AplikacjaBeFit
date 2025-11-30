using BeFit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
        public DbSet<TypCwiczenia> TypyCwiczen { get; set; }
        public DbSet<SesjaTreningowa> SesjeTreningowe { get; set; }
        public DbSet<CwiczenieWSesji> CwiczeniaWSesji { get; set; }
    }
}