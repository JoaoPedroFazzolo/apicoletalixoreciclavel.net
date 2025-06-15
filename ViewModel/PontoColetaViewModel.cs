namespace apicoletalixoreciclavel.ViewModels
{
    public class PontoColetaViewModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public long Capacidade { get; set; }

        public int ColetaCount { get; set; }
    }
}