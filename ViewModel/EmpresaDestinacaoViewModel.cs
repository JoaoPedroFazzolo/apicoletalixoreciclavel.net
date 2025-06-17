namespace apicoletalixoreciclavel.ViewModels
{
    public class EmpresaDestinacaoViewModel
    {
        public long EmpresaDestinacaoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public string DataCriacaoFormatada => DataCriacao.ToString("dd/MM/yyyy HH:mm");
        public string DataAtualizacaoFormatada => DataAtualizacao?.ToString("dd/MM/yyyy HH:mm") ?? "Nunca atualizada";
    }
}