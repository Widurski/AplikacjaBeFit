using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class CwiczenieWSesji
    {
        public int Id { get; set; }

        [Display(Name = "Sesja treningowa")]
        public int SesjaTreningowaId { get; set; }
        public virtual SesjaTreningowa? SesjaTreningowa { get; set; }

        [Display(Name = "Wybierz ćwiczenie")]
        public int TypCwiczeniaId { get; set; }
        public virtual TypCwiczenia? TypCwiczenia { get; set; }

        [Display(Name = "Liczba powtórzeń / Serii")]
        [Required(ErrorMessage = "Podaj liczbę powtórzeń lub serii")]
        [Range(1, 1000)]
        public int LiczbaPowtorzen { get; set; }

        [Display(Name = "Obciążenie (kg)")]
        [Range(0, 500)]
        public double Obciazenie { get; set; }

        
        [Display(Name = "Tempo (np. 5:30 min/km)")]
        public string? Tempo { get; set; }

        [Display(Name = "Zakres tętna (np. 140-150)")]
        public string? ZakresTetna { get; set; }
    }
}