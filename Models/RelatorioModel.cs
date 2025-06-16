using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apicoletalixoreciclavel.Models
{
    [Table("Relatorio")]
    public class RelatorioModel
    {
        [Key]
        public long RelatorioId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nome { get; set; } = string.Empty;
        
        [Required]
        public DateTime DataGeracao { get; set; } = DateTime.Now;
        
        [MaxLength(500)]
        public string? Descricao { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string TipoRelatorio { get; set; } = string.Empty;
    }
}