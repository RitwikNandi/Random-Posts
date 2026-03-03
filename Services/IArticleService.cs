using Random_Posts.Models;

namespace Random_Posts.Services
{
    public interface IArticleService
    {
        public Task<IEnumerable<RandomPost>> GetAllAsync(DateTime? afterTimestamp, int pageSize);
        public Task<RandomPost> GetByIdAsync(Guid id);
    }
}
