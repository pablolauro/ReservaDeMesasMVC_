using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReservaDeMesasMVC_.Models;

namespace ReservaDeMesasMVC_.Controllers
{
    public class ClienteController : Controller
    {
        string BaseUrl = "https://localhost:7233/";
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Clientes()
        {

            List<Cliente>? clientes = new List<Cliente>();

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/clientes");

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;

                clientes = JsonConvert.DeserializeObject<List<Cliente>>(dados);
            }

            return View(clientes);

        }

        public IActionResult CadastrarOuAlterar(int id = 0)
        {
            if (id == 0)
                return View(new Cliente());
            else
            {
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/clientes/" + id.ToString()).Result;

                return View(response.Content.ReadAsAsync<Cliente>().Result);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CadastrarOuAlterar(Cliente p, int id)
        {
            if (id == 0)
            {
                HttpClient client = new HttpClient();

                HttpResponseMessage response = await
                    client.PostAsJsonAsync(BaseUrl + "api/clientes", p);
                TempData["SuccessMessage"] = "Salvo com sucesso";
            } else
            {
                HttpClient client = new HttpClient();

                HttpResponseMessage response = await
                   client.PutAsJsonAsync(BaseUrl + "api/clientes/ " + id, p);
                TempData["SuccessMessage"] = "Salvo com sucesso";
            }

            return RedirectToAction("clientes");
        }       

        public async Task<IActionResult> ExcluirAsync(int id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await
               client.DeleteAsync(BaseUrl + "api/clientes/ " + id);
            TempData["SuccessMessage"] = "Excluido com sucesso";

            return RedirectToAction("clientes");
        }
    }
}
