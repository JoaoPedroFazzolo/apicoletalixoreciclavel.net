using System.ComponentModel.DataAnnotations;

namespace apicoletalixoreciclavel.Models
{
    public class DestinacaoModel
    {
        public long DestinacaoId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string Tipo { get; set; } = string.Empty;
        
        [MaxLength(1000)]
        public string Descricao { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(500)]
        public string Endereco { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string Telefone { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string ResponsavelTecnico { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string Status { get; set; } = "Ativo";
        
        public decimal? CapacidadeMaxima { get; set; }
        
        [MaxLength(50)]
        public string UnidadeCapacidade { get; set; } = "Toneladas";
        
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        
        public DateTime? DataAtualizacao { get; set; }
        
        [MaxLength(500)]
        public string Observacoes { get; set; } = string.Empty;
        
        public bool PermiteColeta { get; set; } = true;
        
        [MaxLength(8)]
        public string? HorarioFuncionamentoInicio { get; set; } 
        
        [MaxLength(8)]
        public string? HorarioFuncionamentoFim { get; set; }   
        
        [MaxLength(200)]
        public string DiasAtendimento { get; set; } = string.Empty;
    }
}