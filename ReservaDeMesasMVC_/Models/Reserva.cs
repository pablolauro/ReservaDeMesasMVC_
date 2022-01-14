using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

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

        [BindProperty(SupportsGet = true)]
        public string? StringdeBusca { get; set; }
        public SelectList? clientes { get; set; }
        [BindProperty(SupportsGet = true)]
        public string clienteSelecioado { get; set; }

        public Reserva()
        {
            data = DateTime.Now.Date;
            if (DateTime.Now.Hour < 16)
            {
                horainicio = DateTime.ParseExact("18:00:00", "HH:mm:ss",
                                        CultureInfo.InvariantCulture);
            } 
            else
            {
                horainicio = DateTime.ParseExact(DateTime.Now.AddHours(1).ToString("HH:mm:ss"),"HH:mm:ss", CultureInfo.InvariantCulture);
            }

            horaFim = DateTime.ParseExact("23:00:00", "HH:mm:ss",
                                        CultureInfo.InvariantCulture);


        }
    }
}
