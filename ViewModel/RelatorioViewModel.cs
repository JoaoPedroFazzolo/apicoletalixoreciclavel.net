namespace apicoletalixoreciclavel.ViewModels
{
    public class RelatorioViewModel
    {
        public long RelatorioId { get; set; }
        public string Nome { get; set; }
        public DateTime DataGeracao { get; set; }
        public string Descricao { get; set; }
        public string TipoRelatorio { get; set; }
    }
}