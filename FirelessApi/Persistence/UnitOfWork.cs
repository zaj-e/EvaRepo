using FirelessApi.Domain.Repository;

namespace FirelessApi.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly FirelessDbContext _context;

    public UnitOfWork(FirelessDbContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}