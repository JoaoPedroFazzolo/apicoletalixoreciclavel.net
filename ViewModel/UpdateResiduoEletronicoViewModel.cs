using System.ComponentModel.DataAnnotations;

namespace apicoletalixoreciclavel.ViewModels
{
    public class UpdateResiduoEletronicoViewModel
    {
        public long ResiduoEletronicoId { get; set; }

        [Required(ErrorMessage = "O tipo é obrigatório")]
        [StringLength(50, ErrorMessage = "O tipo deve ter no máximo 50 caracteres")]
        public string Tipo { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A marca é obrigatória")]
        [StringLength(50, ErrorMessage = "A marca deve ter no máximo 50 caracteres")]
        public string Marca { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O modelo é obrigatório")]
        [StringLength(50, ErrorMessage = "O modelo deve ter no máximo 50 caracteres")]
        public string Modelo { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O estado é obrigatório")]
        [StringLength(20, ErrorMessage = "O estado deve ter no máximo 20 caracteres")]
        public string Estado { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O status é obrigatório")]
        [StringLength(20, ErrorMessage = "O status deve ter no máximo 20 caracteres")]
        public string Status { get; set; } = string.Empty;

        public long UsuarioId { get; set; }
    }
}