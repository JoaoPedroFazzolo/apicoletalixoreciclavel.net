namespace apicoletalixoreciclavel.ViewModels
{
    public class NotificacaoViewModel
    {
        public long NotificacaoId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataLeitura { get; set; }
        public long? UsuarioId { get; set; }
        public string? UsuarioNome { get; set; }
        public string DataCriacaoFormatada => DataCriacao.ToString("dd/MM/yyyy HH:mm");
        public string DataLeituraFormatada => DataLeitura?.ToString("dd/MM/yyyy HH:mm") ?? "";
    }
}