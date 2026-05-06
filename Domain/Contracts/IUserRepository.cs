using Domain.Models;

namespace Domain.Contracts;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<List<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task SaveChangesAsync(CancellationToken ct = default);
}