using LivrariaDoroTechWebApplication.Models;

namespace LivrariaDoroTechWebApplication.Repository.Intefaces
{
    public interface ILivroReporitory
    {
        Task<List<LivroModel>> GetAllLivros(int pagina, int qtd);
        Task<LivroModel> GetById(int id);
        Task<LivroModel> InsertLivros(LivroModel livro);
        Task<LivroModel> UpdateLivros(LivroModel livro, int id);
        Task<bool> DeleteLivro(int id);
    }
}
