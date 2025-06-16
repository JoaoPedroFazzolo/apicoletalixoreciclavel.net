using System.ComponentModel.DataAnnotations;

namespace apicoletalixoreciclavel.Models
{
    public class NotificacaoModel
    {
        public long NotificacaoId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Titulo { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(1000)]
        public string Mensagem { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string Tipo { get; set; } = string.Empty; // Info, Warning, Error, Success
        
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "NaoLida";
        
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        
        public DateTime? DataLeitura { get; set; }
        
        public long? UsuarioId { get; set; }
        
        public virtual UsuarioModel? Usuario { get; set; }
    }
}