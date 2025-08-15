using JWTTraining.Context;
using JWTTraining.Entities;
using JWTTraining.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JWTTraining.Services
{
    public class BlogRepository : IBlogRepository
    {
        private readonly DatabaseContext _context;
        public BlogRepository(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Blog>> GetBlogsListAsync()
        {
            return await _context.Blogs.OrderBy(b => b.Title).ToListAsync();
        }

        public async Task<Blog?> GetBlogByIdAsync(int blogId)
        {
            return await _context.Blogs.FindAsync(blogId);
        }

        public async Task AddBlogAsync(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void RemoveBlog(Blog blog)
        {
            _context.Blogs.Remove(blog);
        }
    }
}
