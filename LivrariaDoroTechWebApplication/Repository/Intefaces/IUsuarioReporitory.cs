using LivrariaDoroTechWebApplication.Models;

using Microsoft.EntityFrameworkCore;

namespace LivrariaDoroTechWebApplication.Repository.Intefaces
{
    public interface IUsuarioReporitory
    {
        Task<UsuarioModel> Authenticate(string username, string password);
        Task<UsuarioModel> LoginValidation(string usuario, string senha);
        Task<UsuarioModel> GetById(int id);
        Task<UsuarioModel> InsertUsuario(UsuarioModel user);
        Task<bool> DeleteUsuario(int id);


    }
}
