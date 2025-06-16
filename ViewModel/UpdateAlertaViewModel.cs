using System.ComponentModel.DataAnnotations;

namespace apicoletalixoreciclavel.ViewModels
{
    public class UpdateAlertaViewModel
    {
        [Required(ErrorMessage = "A mensagem é obrigatória")]
        [MaxLength(500, ErrorMessage = "A mensagem deve ter no máximo 500 caracteres")]
        [MinLength(10, ErrorMessage = "A mensagem deve ter pelo menos 10 caracteres")]
        public string Mensagem { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tipo de alerta é obrigatório")]
        [StringLength(50, ErrorMessage = "O tipo de alerta deve ter no máximo 50 caracteres")]
        public string TipoAlerta { get; set; } = string.Empty;

        [Required(ErrorMessage = "O status é obrigatório")]
        [StringLength(20, ErrorMessage = "O status deve ter no máximo 20 caracteres")]
        public string Status { get; set; } = string.Empty;
        
        public long? UsuarioId { get; set; }
    }
}