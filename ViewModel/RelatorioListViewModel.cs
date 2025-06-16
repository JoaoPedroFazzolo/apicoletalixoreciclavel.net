namespace apicoletalixoreciclavel.ViewModels
{
    public class RelatorioListViewModel
    {
        public long RelatorioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string TipoRelatorio { get; set; } = string.Empty;
        public string DataGeracaoFormatada => DataGeracao.ToString("dd/MM/yyyy HH:mm");
        public DateTime DataGeracao { get; set; }
    }
}