using System.ComponentModel.DataAnnotations;

namespace ReservaDeMesasMVC_.Models
{
    public class Reserva
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage ="Campo obrigatório")]
        [DataType(DataType.Date)]
        [Display(Name ="Data")]
        public DateTime data { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Hora de Início")]
        public DateTime horainicio { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Hora de Término")]
        public DateTime horaFim { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Cliente")]
        public int clienteId { get; set; }
        public Cliente? cliente { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Mesa")]
        public int mesaId { get; set; }
        public Mesa? mesa { get; set; }
    }
}
