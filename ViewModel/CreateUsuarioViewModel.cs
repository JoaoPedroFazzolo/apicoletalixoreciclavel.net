using System.ComponentModel.DataAnnotations;

namespace apicoletalixoreciclavel.ViewModels
{
    public class CreateUsuarioViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(100)]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [MaxLength(100)]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "O telefone é obrigatório")]
        [MaxLength(20)]
        public string Telefone { get; set; }
        
        [Required(ErrorMessage = "O endereço é obrigatório")]
        [MaxLength(200)]
        public string Endereco { get; set; }
        
        [Required(ErrorMessage = "O CEP é obrigatório")]
        [MaxLength(10)]
        public string Cep { get; set; }
        
        [Required(ErrorMessage = "A cidade é obrigatória")]
        [MaxLength(50)]
        public string Cidade { get; set; }
        
        [Required(ErrorMessage = "O estado é obrigatório")]
        [MaxLength(2)]
        public string Estado { get; set; }
        
        [Required(ErrorMessage = "A senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        public string Senha { get; set; }
        
        [MaxLength(50)]
        public string Role { get; set; } = "User";
    }
}