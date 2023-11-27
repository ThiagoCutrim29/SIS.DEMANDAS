using SIGD.Models;

namespace SIGD.seção
{
    public interface Isecao
    {
        void CriarSecao( Usuario usuario );
        void removerSecao();

        Usuario BuscarSecaoUsuarioLogado();

    }
}
