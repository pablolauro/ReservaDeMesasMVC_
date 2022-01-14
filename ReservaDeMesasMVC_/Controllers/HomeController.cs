using Microsoft.AspNetCore.Mvc;
using ReservaDeMesasMVC_.Models;
using System.Diagnostics;

namespace ReservaDeMesasMVC_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string BaseUrl = "https://localhost:7233/";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult CadastrarOuAlterar()
        {

            return View(new PreReserva());

        }


        [HttpPost]
        public async Task<IActionResult> CadastrarOuAlterar(PreReserva p, int id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await
                client.PostAsJsonAsync(BaseUrl + "api/prereservas", p);
            TempData["SuccessMessage"] = "Pré Reserva feita com Sucesso";

            return RedirectToAction("Index");
        }
    }
}