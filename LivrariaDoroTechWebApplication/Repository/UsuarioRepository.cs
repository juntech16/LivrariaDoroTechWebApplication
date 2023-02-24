using LivrariaDoroTechWebApplication.Data;
using LivrariaDoroTechWebApplication.Models;
using LivrariaDoroTechWebApplication.Repository.Intefaces;
using LivrariaDoroTechWebApplication.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LivrariaDoroTechWebApplication.Repository
{
    public class UsuarioRepository : IUsuarioReporitory
    {
        private readonly LivrariaDBContext _dbContext;
        public UsuarioRepository(LivrariaDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UsuarioModel> Authenticate(string username, string password)
        {
            var user = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Login == username && x.Senha == password);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Login),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Senha = tokenHandler.WriteToken(token);

            return user;
        }

        public async Task<UsuarioModel> LoginValidation(string usuario, string senha)
        {
            var user = await _dbContext.Usuarios.FirstOrDefaultAsync(a => a.Login == usuario && a.Senha == senha);

            return user;
        }

        public async Task<UsuarioModel> GetById(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<UsuarioModel> InsertUsuario(UsuarioModel user)
        {
            await _dbContext.Usuarios.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUsuario(int id)
        {
            UsuarioModel userId = await GetById(id);
            if (userId == null)
                throw new Exception($"Usuário não foi encontrado.");

            _dbContext.Usuarios.Remove(userId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
