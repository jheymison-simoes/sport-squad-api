using Microsoft.EntityFrameworkCore;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Repositories;

public class CreateUserRepository : BaseRepository<User>, ICreateUserRepository
{
    public CreateUserRepository(SqlContext db) : base(db)
    {
    }

    public async Task<User> GetByPhoneNumber(string ddd, string phoneNumber)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Ddd == ddd && u.PhoneNumber == phoneNumber);
    }
    
    public async Task<User> GetByEmail(string email)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task<bool> IsDuplicated(string email)
    {
        return await DbSet
            .AsNoTracking()
            .AnyAsync(u => u.Email == email);
    }
}