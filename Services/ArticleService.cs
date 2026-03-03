using Random_Posts.Models;
using Random_Posts.Repositories;
using System.Linq;

namespace Random_Posts.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository repository)
        {
            _articleRepository = repository;
        }

        public async Task<IEnumerable<RandomPost>> GetAllAsync(DateTime? afterTimestamp, int pageSize)
        {
            var articles = await _articleRepository.GetAllAsync();

            var query = articles.OrderByDescending(a=>a.CreateAt);

            if (afterTimestamp.HasValue)
            {
                articles = articles.Where(articles => articles.CreateAt < afterTimestamp.Value).OrderByDescending(a=>a.CreateAt);
            }
            
            return query.Take(pageSize).ToList();
        }

        public Task<RandomPost?> GetByIdAsync(Guid id) => _articleRepository.GetByIdAsync(id);
    }
}
