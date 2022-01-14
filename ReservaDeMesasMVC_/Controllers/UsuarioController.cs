using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReservaDeMesasMVC_.Models;
using System.Security.Claims;

namespace ReservaDeMesasMVC_.Controllers
{
    
    public class UsuarioController : Controller
    {
        string BaseUrl = "https://localhost:7233/";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginPage()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("usuarios");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginPage(Usuario usuario, bool manterlogado)
        {
            try
            {
                //if (ModelState.IsValid)
                {
                    //aqui faz uma consulta conforme os dados de usuário

                    Usuario user = new Usuario();

                    HttpClient client = new HttpClient();

                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("api/usuarios/login/"+usuario.login);

                    if (response.IsSuccessStatusCode)
                    {
                        var dados = response.Content.ReadAsStringAsync().Result;

                        user = JsonConvert.DeserializeObject<Usuario>(dados);
                    }

                    if (user.login == null)
                    {
                        ViewBag.Erro = "Usuário inexistente";
                        return View();
                    }
                        


                    if (usuario.password.Equals(user.password))
                    {
                        //*******************************************

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                            new Claim(ClaimTypes.Name, user.login),
                            new Claim(ClaimTypes.Role,user.tipo)
                        };

                        var identidade = new ClaimsIdentity(claims, "Login");

                        ClaimsPrincipal principal = new ClaimsPrincipal(identidade);

                        var regrasAutenticacao = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(1),
                            IsPersistent = manterlogado
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            principal, regrasAutenticacao
                            );

                        //*******************************************

                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ViewBag.Erro = "Usuário ou senha não conferem ou não existem!";

                    }

                }
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Ocorreu um problema ao autenticar: " + ex.Message;

            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
                //HttpContext.Session.Clear();                
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<ActionResult> Usuarios()
        {

            List<Usuario>? usuarios = new List<Usuario>();

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/usuarios");

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;

                usuarios = JsonConvert.DeserializeObject<List<Usuario>>(dados);
            }

            return View(usuarios);

        }


        [Authorize]
        public IActionResult CadastrarOuAlterar()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CadastrarOuAlterar(Usuario p, int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await
            client.PostAsJsonAsync(BaseUrl + "api/usuarios", p);           

            TempData["SuccessMessage"] = "Salvo com sucesso";

            return RedirectToAction("usuarios");
        }

        [Authorize]
        public async Task<IActionResult> ExcluirAsync(int id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await
               client.DeleteAsync(BaseUrl + "api/usuarios/" + id);
            TempData["SuccessMessage"] = "Excluido com sucesso";

            return RedirectToAction("usuarios");
        }

        
    }
}
