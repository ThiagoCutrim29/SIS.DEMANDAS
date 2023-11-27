using Microsoft.AspNetCore.Mvc;
using SIGD.Models;
using SIGD.seção;
using System.Linq.Expressions;

namespace SIGD.Controllers
{
    public class LoginController : Controller

    {

        private readonly UsuariosController _usuariosController;
        private readonly Isecao _secao;
        public LoginController(UsuariosController usuariosController,
                                Isecao secao)
        {
            _usuariosController = usuariosController;
            _secao = secao;
        }

        public IActionResult Index()
        // se o usuario estiver logando, redirecione para index

        {
            if (_secao.BuscarSecaoUsuarioLogado() != null) return RedirectToAction("Index", "home");
            return View();
        }

        public IActionResult Sair()
            {
            _secao.removerSecao();
            return RedirectToAction("Index", "Login");

            }
        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Usuario usuario = _usuariosController.Buscarlogin(loginModel.Cpf) ;

                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Password))
                        {
                            _secao.CriarSecao(usuario);
                            return RedirectToAction("Index", "Home");

                        }
                        TempData["MensagemErro"] = $"senha invalida";
                    }
                    TempData["MensagemErro"] = $"login ou senha invalidos";
                }

           


                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos realizar seu login. Verifique suas credenciais e tente novamente! {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }


}
