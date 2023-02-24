using LivrariaDoroTechWebApplication.Data;
using LivrariaDoroTechWebApplication.Models;
using LivrariaDoroTechWebApplication.Repository.Intefaces;

using Microsoft.EntityFrameworkCore;

using System.Diagnostics.CodeAnalysis;

namespace LivrariaDoroTechWebApplication.Repository
{
    public class LivroRepository : ILivroReporitory
    {
        private readonly LivrariaDBContext _dbContext;
        public LivroRepository(LivrariaDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<LivroModel>> GetAllLivros(int pagina, int qtd)
        {
            var livros = await _dbContext.Livros.Skip(pagina * qtd).Take(qtd).ToListAsync();

            return livros;
        }

        public async Task<LivroModel> GetById(int id)
        {
            return await _dbContext.Livros.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<LivroModel> InsertLivros(LivroModel livro)
        {
            await _dbContext.Livros.AddAsync(livro);
            await _dbContext.SaveChangesAsync();

            return livro;
        }

        public async Task<LivroModel> UpdateLivros(LivroModel livro, int id)
        {
            LivroModel livroId = await GetById(id);
            if (livroId == null)
                throw new Exception($"Livro {livro.Nome} não foi encontrado.");

            livroId.Nome = livro.Nome;
            livroId.DataCad = DateTime.Now;

            _dbContext.Livros.Update(livroId);
            await _dbContext.SaveChangesAsync();

            return livroId;

        }
        public async Task<bool> DeleteLivro(int id)
        {
            LivroModel livroId = await GetById(id);
            if (livroId == null)
                throw new Exception($"Livro não foi encontrado.");

            _dbContext.Livros.Remove(livroId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
