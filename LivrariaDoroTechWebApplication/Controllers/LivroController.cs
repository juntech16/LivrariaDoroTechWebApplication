using LivrariaDoroTechWebApplication.Models;
using LivrariaDoroTechWebApplication.Repository.Intefaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaDoroTechWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroReporitory _livroReporitory;
        private readonly ILogger<LivroController> _logger;

        public LivroController(ILivroReporitory livroReporitory, ILogger<LivroController> logger)
        {
            _livroReporitory = livroReporitory ?? throw new ArgumentNullException(nameof(livroReporitory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<LivroModel>>> ListarTodosLivros(int pagina, int qtd)
        {
            try
            {
                _logger.LogInformation($"Pagina {pagina}, Quantidade {qtd}");

                List<LivroModel> livros = await _livroReporitory.GetAllLivros(pagina, qtd);

                return Ok(livros);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<LivroModel>> ListarLivroPorId(int id)
        {
            try
            {
                _logger.LogInformation($"LivroPorId {id}");

                LivroModel livro = await _livroReporitory.GetById(id);

                return Ok(livro);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<LivroModel>> CadastrarLivro([FromBody] LivroModel livroModel)
        {
            try
            {
                _logger.LogInformation($"Dados {livroModel}");

                LivroModel livro = await _livroReporitory.InsertLivros(livroModel);

                return Ok(livro);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<LivroModel>> AtualizarLivro([FromBody] LivroModel livroModel, int id)
        {
            try
            {
                _logger.LogInformation($"Dados {livroModel}");

                livroModel.Id = id;
                LivroModel livro = await _livroReporitory.UpdateLivros(livroModel, id);

                return Ok(livro);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<LivroModel>> DeletarLivro(int id)
        {
            try
            {
                _logger.LogInformation($"LivroPorId {id}");

                bool delete = await _livroReporitory.DeleteLivro(id);

                return Ok(delete);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
