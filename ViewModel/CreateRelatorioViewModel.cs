using System.ComponentModel.DataAnnotations;

namespace apicoletalixoreciclavel.ViewModels
{
    public class CreateRelatorioViewModel
    {
        [Required(ErrorMessage = "O nome do relatório é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Name { get; set; }
     
    }
}