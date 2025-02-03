using System.Diagnostics;
using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ServiceRepository(DataContext context) : BaseRepository<ServiceEntity>(context), IServiceRepository
{
    public override async Task<IEnumerable<ServiceEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Services
                .Include(x => x.UnitType)
                .ToListAsync();

            return entities;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving entities :: {ex.Message}");
            return null!;
        }
    }

    public override async Task<ServiceEntity> GetAsync(Expression<Func<ServiceEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try
        {
            var entity = await _context.Services
                .Include(x => x.UnitType)
                .FirstOrDefaultAsync(expression);

            if (entity == null) 
                return null!;

            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving service :: {ex.Message}");
            return null!;
        }
    }
}