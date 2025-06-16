using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apicoletalixoreciclavel.Models
{
    [Table("Empresa_destinacao")]
    public class EmpresaDestinacaoModel
    {
        [Key]
        public long EmpresaDestinacaoId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(500)]
        public string Endereco { get; set; } = string.Empty;
        
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        
        public DateTime? DataAtualizacao { get; set; }
    }
}