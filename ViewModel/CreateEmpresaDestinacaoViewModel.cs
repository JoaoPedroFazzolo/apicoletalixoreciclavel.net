using System.ComponentModel.DataAnnotations;

namespace apicoletalixoreciclavel.ViewModels
{
    public class CreateEmpresaDestinacaoViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres")]
        public string Nome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O endereço é obrigatório")]
        [MaxLength(500, ErrorMessage = "O endereço deve ter no máximo 500 caracteres")]
        public string Endereco { get; set; } = string.Empty;
    }
}