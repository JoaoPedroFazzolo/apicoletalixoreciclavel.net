namespace apicoletalixoreciclavel.ViewModels
{
    public class NotificacaoViewModel
    {
        public long NotificacaoId { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public DateTime DataCriacao { get; set; }
        
        public long? UsuarioId { get; set; }
        
        public string DataCriacaoFormatada => DataCriacao.ToString("dd/MM/yyyy HH:mm");
    }
}