using System.ComponentModel.DataAnnotations;

public class UpdateRelatorioViewModel
{
    public long RelatorioId { get; set; }
    
    [Required(ErrorMessage = "O nome do relatório é obrigatório")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
    public string Name { get; set; }
}

public class RelatorioListViewModel
{
    public long RelatorioId { get; set; }
    public string Name { get; set; }
    public string DataGeracaoFormatada => DataGeracao.ToString("dd/MM/yyyy HH:mm");
    public DateTime DataGeracao { get; set; }
}