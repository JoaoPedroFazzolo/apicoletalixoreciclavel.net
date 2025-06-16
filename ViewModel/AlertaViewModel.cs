using System.ComponentModel.DataAnnotations;

namespace apicoletalixoreciclavel.ViewModels
{
    public class AlertaViewModel
    {
        public long AlertaId { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public DateTime DataAlerta { get; set; }
        public string TipoAlerta { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public long? UsuarioId { get; set; }
        public string? NomeUsuario { get; set; }
    }
}