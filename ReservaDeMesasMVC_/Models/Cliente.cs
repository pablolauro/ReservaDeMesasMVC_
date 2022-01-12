using System.ComponentModel.DataAnnotations;

namespace ReservaDeMesasMVC_.Models
{
    public class Cliente
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string nome { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido")]
        public string email { get; set; }
        [Phone(ErrorMessage = "Informe um  telefone válido")]
        public string telefone { get; set; }
        public List<Reserva>? reservas { get; set; }
        public int? usuarioId { get; set; }
        public Usuario? usuario { get; set; }
    }
}
