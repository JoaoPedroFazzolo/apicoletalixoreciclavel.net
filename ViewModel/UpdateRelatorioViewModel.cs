using System.ComponentModel.DataAnnotations;

namespace apicoletalixoreciclavel.ViewModels
{
    public class UpdateRelatorioViewModel
    {
        public long RelatorioId { get; set; }
        
        [Required(ErrorMessage = "O nome do relatório é obrigatório")]
        [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres")]
        public string Nome { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        public string? Descricao { get; set; }
        
        [Required(ErrorMessage = "O tipo do relatório é obrigatório")]
        [StringLength(50, ErrorMessage = "O tipo deve ter no máximo 50 caracteres")]
        public string TipoRelatorio { get; set; } = string.Empty;
    }
}