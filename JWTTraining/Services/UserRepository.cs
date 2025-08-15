using JWTTraining.Context;
using JWTTraining.Entities;
using JWTTraining.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JWTTraining.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User?> GetUserAsync(string username, string password)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u => u.Username.Equals(username) && u.Password.Equals(password));
        }
    }
}
