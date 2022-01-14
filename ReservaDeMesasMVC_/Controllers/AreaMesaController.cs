using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReservaDeMesasMVC_.Models;

namespace ReservaDeMesasMVC_.Controllers
{
    [Authorize]
    public class AreaMesaController : Controller
    {

        string BaseUrl = "https://localhost:7233/";
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> AreaMesas()
        {
            
            List<AreaMesa>? areasmesas = new List<AreaMesa>();

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/areamesas");

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;

                areasmesas = JsonConvert.DeserializeObject<List<AreaMesa>>(dados);
            }

            return View(areasmesas);

        }

        public IActionResult Cadastrar()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Cadastrar(AreaMesa p)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await
                client.PostAsJsonAsync(BaseUrl + "api/areamesas", p);
            TempData["SuccessMessage"] = "Salvo com sucesso";

            return RedirectToAction("areamesas");
        }

        public IActionResult Alterar(int id)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/areamesas/" + id.ToString()).Result;

            return View(response.Content.ReadAsAsync<AreaMesa>().Result);            
        }

        [HttpPost]
        public async Task<IActionResult> Alterar(AreaMesa m, int id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await
               client.PutAsJsonAsync(BaseUrl + "api/areamesas/ " + id, m);
            TempData["SuccessMessage"] = "Salvo com sucesso";

            return RedirectToAction("areamesas");

        }

        public async Task<IActionResult> ExcluirAsync(int id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await
               client.DeleteAsync(BaseUrl + "api/areamesas/ " + id);
            TempData["SuccessMessage"] = "Excluido com sucesso";

            return RedirectToAction("areamesas");
        }
    }
}
