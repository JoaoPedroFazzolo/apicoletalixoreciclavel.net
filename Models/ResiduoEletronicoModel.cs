namespace apicoletalixoreciclavel.Models;

public class ResiduoEletronicoModel
{
    public long ResiduoEletronicoId { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    
    public long UsuarioId { get; set; }
    public UsuarioModel Usuario { get; set; } = null!;
}