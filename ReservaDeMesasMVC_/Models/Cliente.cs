using System.ComponentModel.DataAnnotations;

namespace ReservaDeMesasMVC_.Models
{
    public class Cliente
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome muito curto")]
        [Display(Name = "Nome:")]
        public string nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido")]
        [Display(Name = "E-mail:")]
        public string email { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "Informe um  telefone válido")]
        [Display(Name = "Celular:")]
        public string telefone { get; set; }
        public List<Reserva>? reservas { get; set; }
        public int? usuarioId { get; set; }
        public Usuario? usuario { get; set; }


        public Cliente()
        {
            nome = string.Empty;
            email = string.Empty;
            telefone = string.Empty;
        }
    }
}
