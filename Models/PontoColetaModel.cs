using System.ComponentModel.DataAnnotations.Schema;

namespace apicoletalixoreciclavel.Models;

[Table("Ponto_coleta")]
public class PontoColetaModel
{
    public long PontoColetaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public long Capacidade { get; set; }

    public ICollection<ColetaModel> Coletas { get; set; } = new List<ColetaModel>();
}