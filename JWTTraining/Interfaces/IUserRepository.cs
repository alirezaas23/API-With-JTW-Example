using JWTTraining.Entities;

namespace JWTTraining.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync(string username, string password);
    }
}
