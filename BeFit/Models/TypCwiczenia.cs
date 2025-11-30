using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class TypCwiczenia
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa ćwiczenia")]
        [Required(ErrorMessage = "Proszę podać nazwę ćwiczenia")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nazwa musi mieć od 3 do 50 znaków")]
        public string Nazwa { get; set; }

        [Display(Name = "Opis techniki")]
        public string? Opis { get; set; }

        
        public virtual ICollection<CwiczenieWSesji>? CwiczeniaWSesjach { get; set; }
    }
}