using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apicoletalixoreciclavel.Models
{
    [Table("Usuario")]
    public class UsuarioModel
    {
        [Key]
        public long UsuarioId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

         [Required]
        [MaxLength(255)]
        public string Senha { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(20)]
        public string Telefone { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(200)]
        public string Endereco { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string Cep { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string Cidade { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(2)]
        public string Estado { get; set; } = string.Empty;
        
       
        
        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = "User";
        
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        
        public virtual ICollection<ResiduoEletronicoModel> ResiduosEletronicos { get; set; } = new List<ResiduoEletronicoModel>();
    }
}