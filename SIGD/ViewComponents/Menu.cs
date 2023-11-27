using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SIGD.Models;

namespace SIGD.ViewComponents
{
    public class Menu:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
            
        {
            string secaoUsuario = HttpContext.Session.GetString("SecaoUsuarioLogado");
        
            if (string.IsNullOrEmpty(secaoUsuario)) return null;

            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(secaoUsuario);
            return View(usuario);
        }
    }
}
