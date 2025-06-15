using System.ComponentModel.DataAnnotations;

namespace apicoletalixoreciclavel.ViewModels
{
    public class AlertaViewModel
    {
        public long AlertaId { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataAlerta { get; set; }
        public string TipoAlerta { get; set; }
        public string Status { get; set; }
        public long? UsuarioId { get; set; }
        public string? NomeUsuario { get; set; }
    }

    public class CreateAlertaViewModel
    {
        [Required(ErrorMessage = "A mensagem é obrigatória")]
        [MaxLength(500, ErrorMessage = "A mensagem deve ter no máximo 500 caracteres")]
        [MinLength(10, ErrorMessage = "A mensagem deve ter pelo menos 10 caracteres")]
        public string Mensagem { get; set; }

        [Required(ErrorMessage = "O tipo de alerta é obrigatório")]
        public string TipoAlerta { get; set; }

        public string Status { get; set; } = "Ativo";
        
        public long? UsuarioId { get; set; }
    }

    public class UpdateAlertaViewModel
    {
        [Required(ErrorMessage = "A mensagem é obrigatória")]
        [MaxLength(500, ErrorMessage = "A mensagem deve ter no máximo 500 caracteres")]
        [MinLength(10, ErrorMessage = "A mensagem deve ter pelo menos 10 caracteres")]
        public string Mensagem { get; set; }

        [Required(ErrorMessage = "O tipo de alerta é obrigatório")]
        public string TipoAlerta { get; set; }

        [Required(ErrorMessage = "O status é obrigatório")]
        public string Status { get; set; }
        
        public long? UsuarioId { get; set; }
    }
}