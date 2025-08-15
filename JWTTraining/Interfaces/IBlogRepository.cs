using JWTTraining.Entities;

namespace JWTTraining.Interfaces
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> GetBlogsListAsync();
        Task<Blog?> GetBlogByIdAsync(int blogId);
        Task AddBlogAsync(Blog blog);
        Task SaveChangesAsync();
        void RemoveBlog(Blog blog);
    }
}
