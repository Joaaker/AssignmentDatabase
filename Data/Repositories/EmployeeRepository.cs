using System.Diagnostics;
using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class EmployeeRepository(DataContext context) : BaseRepository<EmployeeEntity>(context), IEmployeeRepository
{
    public override async Task<IEnumerable<EmployeeEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Employees
                .Include(x => x.Role)
                .ToListAsync();

            return entities;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving entities :: {ex.Message}");
            return null!;
        }
    }

    public override async Task<EmployeeEntity> GetAsync(Expression<Func<EmployeeEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try
        {
            var entity = await _context.Employees
                .Include(x => x.Role)
                .FirstOrDefaultAsync(expression);

            return entity ?? null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving employee entity :: {ex.Message}");
            return null!;
        }
    }
}