namespace apicoletalixoreciclavel.Models;

public class ResiduoEletronicoModel
{
    public long ResiduoEletronicoId { get; set; }
    public string Tipo { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Estado { get; set; }
    public string Status { get; set; }
    
    public long UsuarioId { get; set; }
    public UsuarioModel Usuario { get; set; } = null!;
}