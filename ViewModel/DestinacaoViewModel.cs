namespace apicoletalixoreciclavel.ViewModels
{
    public class DestinacaoViewModel
    {
        public long DestinacaoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ResponsavelTecnico { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal? CapacidadeMaxima { get; set; }
        public string UnidadeCapacidade { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public string Observacoes { get; set; } = string.Empty;
        public bool PermiteColeta { get; set; }
        public TimeSpan? HorarioFuncionamentoInicio { get; set; }
        public TimeSpan? HorarioFuncionamentoFim { get; set; }
        public string DiasAtendimento { get; set; } = string.Empty;
        public string DataCadastroFormatada => DataCadastro.ToString("dd/MM/yyyy");
        public string DataAtualizacaoFormatada => DataAtualizacao?.ToString("dd/MM/yyyy") ?? "";
        public string HorarioFuncionamento => 
            HorarioFuncionamentoInicio.HasValue && HorarioFuncionamentoFim.HasValue 
                ? $"{HorarioFuncionamentoInicio:hh\\:mm} às {HorarioFuncionamentoFim:hh\\:mm}" 
                : "Não informado";
        public string CapacidadeFormatada => 
            CapacidadeMaxima.HasValue 
                ? $"{CapacidadeMaxima:N2} {UnidadeCapacidade}" 
                : "Não informado";
    }
}