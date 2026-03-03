using Random_Posts.Models;

namespace Random_Posts.Repositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<RandomPost>> GetAllAsync();
        Task<RandomPost?> GetByIdAsync(Guid id);
        Task AddAsync (RandomPost randomPost);
        Task UpdateAsync (RandomPost randomPost);
        Task DeleteAsync (Guid id);
    }
}
