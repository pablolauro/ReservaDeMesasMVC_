using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ReservaDeMesasMVC_.Models;

namespace ReservaDeMesasMVC_.Controllers
{
    [Authorize]
    public class MesaController : Controller
    {
        string BaseUrl = "https://localhost:7233/";
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Mesas()
        {

            List<Mesa>? mesas = new List<Mesa>();

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/mesas");

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;

                mesas = JsonConvert.DeserializeObject<List<Mesa>>(dados);
            }

            return View(mesas);

        }

        public IActionResult CadastrarOuAlterar(int id = 0)
        {
            List<AreaMesa>? areasmesas = new List<AreaMesa>();

            HttpClient clientAreaMesa = new HttpClient();

            clientAreaMesa.BaseAddress = new Uri(BaseUrl);
            clientAreaMesa.DefaultRequestHeaders.Clear();
            clientAreaMesa.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseAreaMesa = clientAreaMesa.GetAsync("api/areamesas").Result;

            if (responseAreaMesa.IsSuccessStatusCode)
            {
                var dados = responseAreaMesa.Content.ReadAsStringAsync().Result;

                areasmesas = JsonConvert.DeserializeObject<List<AreaMesa>>(dados);

                ViewBag.idAreaMesa = new SelectList(areasmesas, "id", "nome");
            }

            
            if (id == 0)
                return View(new Mesa());
            else
            {
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/mesas/" + id.ToString()).Result;

                return View(response.Content.ReadAsAsync<Mesa>().Result);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CadastrarOuAlterar(Mesa p, int id)
        {
            HttpClient client = new HttpClient();

            if (id == 0)
            {
                HttpResponseMessage response = await
                    client.PostAsJsonAsync(BaseUrl + "api/mesas", p);
                TempData["SuccessMessage"] = "Salvo com sucesso";
            } else
            {
                HttpResponseMessage response = await
                    client.PutAsJsonAsync(BaseUrl + "api/mesas/ " + id, p);
                TempData["SuccessMessage"] = "Salvo com sucesso";
            }

            return RedirectToAction("mesas");
        }


        public async Task<IActionResult> ExcluirAsync(int id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await
               client.DeleteAsync(BaseUrl + "api/mesas/ " + id);
            TempData["SuccessMessage"] = "Excluido com sucesso";

            return RedirectToAction("mesas");
        }
    }
}
