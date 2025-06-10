namespace apicoletalixoreciclavel.ViewModels
{
    public class CreateResiduoEletronicoViewModel
    {
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Estado { get; set; }
        
        public string Status { get; set; }

        public long UsuarioId { get; set; }
    }
}