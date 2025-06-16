using System.ComponentModel.DataAnnotations;

namespace apicoletalixoreciclavel.ViewModels
{
    public class UpdateNotificacaoViewModel
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        [MaxLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres")]
        public string Titulo { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A mensagem é obrigatória")]
        [MaxLength(1000, ErrorMessage = "A mensagem deve ter no máximo 1000 caracteres")]
        public string Mensagem { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O tipo é obrigatório")]
        [MaxLength(50, ErrorMessage = "O tipo deve ter no máximo 50 caracteres")]
        public string Tipo { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O status é obrigatório")]
        [MaxLength(50, ErrorMessage = "O status deve ter no máximo 50 caracteres")]
        public string Status { get; set; } = string.Empty;
    }
}