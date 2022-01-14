using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReservaDeMesasMVC_.Models;

namespace ReservaDeMesasMVC_.Controllers
{
    public class PreReservaController : Controller
    {
        string BaseUrl = "https://localhost:7233/";
        public IActionResult Index()
        {
            return View();
        }
        
        [Authorize]
        public async Task<ActionResult> PreReservas()
        {

            List<PreReserva>? reservas = new List<PreReserva>();

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/prereservas/data");

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;

                reservas = JsonConvert.DeserializeObject<List<PreReserva>>(dados);
            }

            return View(reservas);

        }

        [Authorize]
        public async Task<IActionResult> ExcluirAsync(int id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await
               client.DeleteAsync(BaseUrl + "api/prereservas/ " + id);
            TempData["SuccessMessage"] = "Excluido com sucesso";

            return RedirectToAction("prereservas");
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
            TempData["SuccessMessage"] = "Salvo com sucesso";
                
            return RedirectToAction("prereservas");
        }
    }
}
