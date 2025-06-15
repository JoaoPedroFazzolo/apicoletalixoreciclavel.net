namespace apicoletalixoreciclavel.ViewModels
{
    public class ColetaViewModel
    {
        public long Id { get; set; }
        public DateTime Data_Coleta { get; set; }
        public long Residuo_Id { get; set; }
        public long Ponto_Coleta_Id { get; set; }

        public string PontoColetaNome { get; set; }
        public string ResiduoTipo { get; set; }
    }
}