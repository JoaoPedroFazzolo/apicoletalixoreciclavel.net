namespace apicoletalixoreciclavel.Models;

public class UsuarioModel
{
    public long UsuarioId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Role { get; set; }

    public List<ResiduoEletronicoModel> ListaResiduos { get; set; } = new();
}