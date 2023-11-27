 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SIGD.Models;
using System.Text.Json;

namespace SIGD.Filters
{
    public class PaginaUsuarioLogado : ActionFilterAttribute

    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string SessaoUsuario = context.HttpContext.Session.GetString("SecaoUsuarioLogado");
            if (string.IsNullOrEmpty(SessaoUsuario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary{
                    { "controller", "Login" }, {"action", "Index" } });
            }
            else
            {
               Usuario usuario = JsonSerializer.Deserialize<Usuario>(SessaoUsuario);
                if (usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary{
                    { "controller", "Login" }, {"action", "Index" } });
                }
            }
            base.OnActionExecuted(context);
        }

    }
}
