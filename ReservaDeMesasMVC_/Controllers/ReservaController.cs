using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ReservaDeMesasMVC_.Models;
using System.Globalization;

namespace ReservaDeMesasMVC_.Controllers
{
    [Authorize]
    public class ReservaController : Controller
    {

        string BaseUrl = "https://localhost:7233/";
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Reservas()
        {

            List<Reserva>? reservas = new List<Reserva>();

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/reservas/data");

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;

                reservas = JsonConvert.DeserializeObject<List<Reserva>>(dados);
            }

            return View(reservas);

        }

        public IActionResult CadastrarOuAlterar(int id = 0, string datapreserva = "")
        {
            List<Cliente>? clientes = new List<Cliente>();
            List<Mesa>? mesas = new List<Mesa>();

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseClientes = client.GetAsync("api/clientes/").Result;

            HttpResponseMessage responseMesas = client.GetAsync("api/mesas/funcionando").Result;

            if (responseClientes.IsSuccessStatusCode)
            {
                var dados = responseClientes.Content.ReadAsStringAsync().Result;

                clientes = JsonConvert.DeserializeObject<List<Cliente>>(dados);

                dados = responseMesas.Content.ReadAsStringAsync().Result;

                mesas = JsonConvert.DeserializeObject<List<Mesa>>(dados);                

                ViewBag.clienteId = new SelectList(clientes, "id", "nome");

                ViewBag.mesaId = new SelectList(mesas, "id", "exibirMesa"); //"numMesa"
            }

            if (id == 0)
            {
                Reserva r = new Reserva();


                if (!string.IsNullOrEmpty(datapreserva))
                {
                    r.data = DateTime.ParseExact(datapreserva, "MM/dd/yyyy HH:mm:ss",
                                        null);
                }

                return View(r);

            }

            else
            {
                HttpResponseMessage response = client.GetAsync("api/reservas/" + id.ToString()).Result;

                return View(response.Content.ReadAsAsync<Reserva>().Result);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CadastrarOuAlterar(Reserva p, int id)
        {
            if (id == 0)
            {
                HttpClient client = new HttpClient();

                HttpResponseMessage response = await
                    client.PostAsJsonAsync(BaseUrl + "api/reservas", p);
                TempData["SuccessMessage"] = "Salvo com sucesso";
            }
            else
            {
                HttpClient client = new HttpClient();

                HttpResponseMessage response = await
                   client.PutAsJsonAsync(BaseUrl + "api/reservas/ " + id, p);
                TempData["SuccessMessage"] = "Salvo com sucesso";
            }

            return RedirectToAction("reservas");
        }

        public async Task<IActionResult> ExcluirAsync(int id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await
               client.DeleteAsync(BaseUrl + "api/reservas/ " + id);
            TempData["SuccessMessage"] = "Excluido com sucesso";

            return RedirectToAction("reservas");
        }
    }
}
