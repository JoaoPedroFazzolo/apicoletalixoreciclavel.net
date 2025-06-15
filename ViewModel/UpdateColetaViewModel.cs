namespace apicoletalixoreciclavel.ViewModels
{
    public class UpdateColetaViewModel
    {
        public long Id { get; set; }

        public DateTime DataColeta { get; set; }
        public long ResiduoId { get; set; }
        public long PontoColetaId { get; set; }
    }
}