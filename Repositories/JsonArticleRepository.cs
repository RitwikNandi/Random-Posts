using Random_Posts.Models;
using System.Text.Json;

namespace Random_Posts.Repositories
{
    public class JsonArticleRepository : IArticleRepository
    {
        private readonly List<RandomPost> _articles;

        public JsonArticleRepository(IWebHostEnvironment env)
        {
            var path = Path.Combine(env.ContentRootPath, "Data", "satire_post.json");

            var articles = File.ReadAllText(path);

            _articles = JsonSerializer.Deserialize<List<RandomPost>>(articles)
                ?? new List<RandomPost>();
        }

        public Task<IEnumerable<RandomPost>> GetAllAsync() => Task.FromResult(_articles.AsEnumerable());

        public Task<RandomPost?> GetByIdAsync(Guid id) => Task.FromResult(_articles.FirstOrDefault(x => x.Guid == id));

        public Task AddAsync(RandomPost post)
        {
            _articles.Add(post);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(RandomPost post)
        {
            var existing = _articles.FirstOrDefault(exist => exist.Guid == post.Guid);

            if (existing != null)
            {
                existing.Title = post.Title;
                existing.Description = post.Description;
                existing.Category = post.Category;
                existing.CreateAt = DateTime.Now;
            }

            return Task.CompletedTask;

        }

        public Task DeleteAsync(Guid id)
        {
            var existing = _articles.FirstOrDefault(exist => exist.Guid ==  id);

            if(existing != null)
            {
                _articles.Remove(existing);
            }

            return Task.CompletedTask;
        }
    }
}
