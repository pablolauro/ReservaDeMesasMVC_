using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReservaDeMesasMVC_.Models
{
    public class AreaMesa
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage ="Campo obrigatório")]
        [StringLength(100, MinimumLength = 1)]
        [DisplayName("Nome Area")]
        public string nome { get; set; }
        public List<Mesa>? mesas { get; set; }
    }
}
