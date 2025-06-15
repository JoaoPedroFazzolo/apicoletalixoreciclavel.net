namespace apicoletalixoreciclavel.Models;

public class PontoColetaModel
{
    public long PontoColetaId { get; set; }
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public long Capacidade { get; set; }

    public virtual ICollection<ColetaModel> Coletas { get; set; }
}