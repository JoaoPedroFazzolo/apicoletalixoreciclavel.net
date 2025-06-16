namespace apicoletalixoreciclavel.Models;

public class ColetaModel
{
    public long ColetaId { get; set; }
    public DateTime DataColeta { get; set; }
    public long ResiduoId { get; set; }
    public long PontoColetaId { get; set; }
    
    public ResiduoEletronicoModel Residuo { get; set; } = null!;
    public PontoColetaModel PontoColeta { get; set; } = null!;
}