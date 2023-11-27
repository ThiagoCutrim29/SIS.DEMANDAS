using Newtonsoft.Json;
using SIGD.Models;
using System.Drawing;

namespace SIGD.seção
{
    public class Secao : Isecao
    {
   
    public readonly IHttpContextAccessor _httpContext;
        public Secao(IHttpContextAccessor httpContext)
    {
            _httpContext= httpContext;

    }
    
        public Usuario BuscarSecaoUsuarioLogado()
        {
            string SecaoUsuario= _httpContext.HttpContext.Session.GetString("SecaoUsuarioLogado");
            if (string.IsNullOrEmpty(SecaoUsuario)) return null;
            return JsonConvert.DeserializeObject<Usuario>(SecaoUsuario);
        }

        public void CriarSecao(Usuario usuario)
        {
            string valor=JsonConvert.SerializeObject(usuario);
            _httpContext.HttpContext.Session.SetString("SecaoUsuarioLogado", valor);
        }

        public void removerSecao()
        {
            _httpContext.HttpContext.Session.Remove("SecaoUsuarioLogado");
        }
    }
}
