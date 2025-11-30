using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BeFit.Models
{
    public class SesjaTreningowa
    {
        public int Id { get; set; }

        [Display(Name = "Data treningu")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Data jest wymagana")]
        public DateTime Data { get; set; }

        [Display(Name = "Komentarz do sesji")]
        [StringLength(200, ErrorMessage = "Komentarz może mieć maks. 200 znaków")]
        public string? Komentarz { get; set; }

        
        public string? UserId { get; set; }
        public virtual IdentityUser? User { get; set; }

        
        public virtual ICollection<CwiczenieWSesji>? Cwiczenia { get; set; }
    }
}