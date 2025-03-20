using System.ComponentModel.DataAnnotations;

namespace HorizonCruises.web.ViewModels
{
    public class ViewModelInput
    {
        [Display(Name = "Identificador del Puerto")]
        public int Id { get; set; }

        [Display(Name = "Puerto")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Pais")]
        public string Pais { get; set; } = null!;

    }
}
