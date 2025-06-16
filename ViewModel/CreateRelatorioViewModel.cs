using System.ComponentModel.DataAnnotations;

namespace apicoletalixoreciclavel.ViewModels
{
    public class CreateRelatorioViewModel
    {
        [Required(ErrorMessage = "O nome do relatório é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "A descricao do relatório é obrigatório")]
        [StringLength(500, ErrorMessage = "O descricao deve ter no máximo 500 caracteres")]
        public string Descricao { get; set; }
     
        [Required(ErrorMessage = "O tipo do relatório é obrigatório")]
        [StringLength(100, ErrorMessage = "O tipo deve ter no máximo 100 caracteres")]
        public string TipoRelatorio { get; set; }
    }
}