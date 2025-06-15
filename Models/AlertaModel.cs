using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apicoletalixoreciclavel.Models
{
    [Table("Alertas")]
    public class AlertaModel
    {
        [Key]
        public long AlertaId { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Mensagem { get; set; }
        
        [Required]
        public DateTime DataAlerta { get; set; } = DateTime.Now;
        
        [Required]
        [MaxLength(50)]
        public string TipoAlerta { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Ativo";
        
        public long? UsuarioId { get; set; }
        
        [ForeignKey("UsuarioId")]
        public virtual UsuarioModel? Usuario { get; set; }
    }
    
    public static class TipoAlerta
    {
        public const string ColetaAtrasada = "ColetaAtrasada";
        public const string LixeiraCheia = "LixeiraCheia";
        public const string ManutencaoNecessaria = "ManutencaoNecessaria";
        public const string AreaCritica = "AreaCritica";
        public const string Outros = "Outros";
    }
    
    public static class StatusAlerta
    {
        public const string Ativo = "Ativo";
        public const string Resolvido = "Resolvido";
        public const string EmAndamento = "EmAndamento";
    }
}