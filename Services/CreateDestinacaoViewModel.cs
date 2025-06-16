using System.ComponentModel.DataAnnotations;

namespace apicoletalixoreciclavel.ViewModels
{
    public class CreateDestinacaoViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres")]
        public string Nome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O tipo é obrigatório")]
        [MaxLength(100, ErrorMessage = "O tipo deve ter no máximo 100 caracteres")]
        public string Tipo { get; set; } = string.Empty;
        
        [MaxLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres")]
        public string Descricao { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O endereço é obrigatório")]
        [MaxLength(500, ErrorMessage = "O endereço deve ter no máximo 500 caracteres")]
        public string Endereco { get; set; } = string.Empty;
        
        [MaxLength(50, ErrorMessage = "O telefone deve ter no máximo 50 caracteres")]
        public string Telefone { get; set; } = string.Empty;
        
        [MaxLength(200, ErrorMessage = "O email deve ter no máximo 200 caracteres")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = string.Empty;
        
        [MaxLength(200, ErrorMessage = "O responsável técnico deve ter no máximo 200 caracteres")]
        public string ResponsavelTecnico { get; set; } = string.Empty;
        
        [Range(0, double.MaxValue, ErrorMessage = "A capacidade máxima deve ser maior que zero")]
        public decimal? CapacidadeMaxima { get; set; }
        
        [MaxLength(50, ErrorMessage = "A unidade de capacidade deve ter no máximo 50 caracteres")]
        public string UnidadeCapacidade { get; set; } = "Toneladas";
        
        [MaxLength(500, ErrorMessage = "As observações devem ter no máximo 500 caracteres")]
        public string Observacoes { get; set; } = string.Empty;
        
        public bool PermiteColeta { get; set; } = true;
        
        public TimeSpan? HorarioFuncionamentoInicio { get; set; }
        
        public TimeSpan? HorarioFuncionamentoFim { get; set; }
        
        [MaxLength(200, ErrorMessage = "Os dias de atendimento devem ter no máximo 200 caracteres")]
        public string DiasAtendimento { get; set; } = string.Empty;
    }
}