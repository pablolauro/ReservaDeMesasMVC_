using System.ComponentModel.DataAnnotations;

namespace ReservaDeMesasMVC_.Models
{
    public class Mesa
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(2, 12, ErrorMessage = "A quantidade de lugares deve ser entre 2 e 12")]
        [Display(Name = "Quantidade de Lugares:")]
        public int qtdlugares { get; set; }
        [Required(ErrorMessage ="Campo obrigatório")]
        [Range(1, 999)]
        [Display(Name = "Nº da Mesa:")]
        public int numMesa { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Mesa em funcionamento:")]
        public bool funcionando { get; set; }
        [Display(Name = "Area da Mesa:")]
        public int idAreaMesa { get; set; }
        public AreaMesa? area { get; set; }
        public List<Reserva>? reservas { get; set; }
    }
}
