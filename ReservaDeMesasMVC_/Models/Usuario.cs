using System.ComponentModel.DataAnnotations;

namespace ReservaDeMesasMVC_.Models
{
    public class Usuario
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage ="Campo obrigatório")]
        [Display(Name = "Login:")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O login deve possuir no mínimo 5 caracteres")]
        public string login { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Senha:")]
        [StringLength(100, MinimumLength = 5, ErrorMessage ="A senha deve possuir no mínimo 5 caracteres")]
        public string password { get; set; }
        [StringLength(3, MinimumLength = 3, ErrorMessage = "A senha deve possuir no mínimo 5 caracteres")]
        public string tipo { get; set; }
        public Cliente? cliente { get; set; }
    }
}
