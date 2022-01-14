using System.ComponentModel.DataAnnotations;

namespace ReservaDeMesasMVC_.Models
{
    public class PreReserva
    {
        [Key]
        public int id { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public DateTime data { get; set; }
        [Display(Name = "Nome")]
        public string nomecliente { get; set; }
        [EmailAddress(ErrorMessage = "Informe um e-mail válido")]
        [Display(Name = "E-mail")]
        public string emailcliente { get; set; }
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "Informe um  telefone válido")]
        [Display(Name = "Telefone Celular")]
        public string fonecliente { get; set; }
        [Display(Name = "Observacao")]
        public string observacao { get; set; }
    }
}
