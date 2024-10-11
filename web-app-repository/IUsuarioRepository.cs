using web_app_domain;

namespace web_app_repository
{
    public  interface IUsuarioRepository 
    {
        Task<IEnumerable<Usuario>> ListarUsarios();
        Task SalvarUsario(Usuario usuario);
        Task AtualizarUsuario(Usuario usuario);
        Task RemoverUsuario(int id);
    }
}
