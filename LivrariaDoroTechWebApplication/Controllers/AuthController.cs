using LivrariaDoroTechWebApplication.Models;
using LivrariaDoroTechWebApplication.Repository.Intefaces;
using LivrariaDoroTechWebApplication.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaDoroTechWebApplication.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioReporitory _usuarioReporitory;
        public AuthController(IUsuarioReporitory usuarioReporitory)
        {
            _usuarioReporitory = usuarioReporitory;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Auth([FromBody] UsuarioModel model)
        {
            try
            {
                UsuarioModel user = await _usuarioReporitory.Authenticate(model.Login, model.Senha);

                if (user == null)
                    return BadRequest(new { message = "Usuário ou senha inválidos" });

                var token = TokenServices.GenerateToken(user);
                if (token == null)
                    return BadRequest(new { message = "Não foi possível gerar o token. Tente novamente." });

                return new 
                {
                    Login = user.Login,
                    Token = token.ToString()
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<UsuarioModel>> ListarUsuarioPorId(int id)
        //{
        //    UsuarioModel user = await _usuarioReporitory.GetById(id);

        //    return Ok(user);
        //}


        //[HttpPost]
        //public async Task<ActionResult<LivroModel>> Login(string usuario, string senha)
        //{
        //    UsuarioModel user = await _usuarioReporitory.LoginValidation(usuario, senha);

        //    return Ok(user);
        //}

        //[HttpPost]
        //public async Task<ActionResult<UsuarioModel>> CadastrarUsuario([FromBody] UsuarioModel usuarioModel)
        //{
        //    //var pass = Funcoes.CriptPassword(password);

        //    UsuarioModel usuario = await _usuarioReporitory.InsertUsuario(usuarioModel);

        //    return Ok(usuario);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<LivroModel>> AtualizarLivro([FromBody] LivroModel livroModel, int id)
        //{
        //    livroModel.Id = id;
        //    LivroModel livro = await _livroReporitory.UpdateLivros(livroModel, id);

        //    return Ok(livro);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<LivroModel>> DeletarLivro(int id)
        //{
        //    bool delete = await _livroReporitory.DeleteLivro(id);

        //    return Ok(delete);
        //}


    }
}
