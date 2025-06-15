namespace apicoletalixoreciclavel.ViewModels
{
    public class UsuarioViewModel
    {
        public long UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}