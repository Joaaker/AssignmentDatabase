using System.Diagnostics;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class RoleRepository(DataContext context) : BaseRepository<RoleEntity>(context), IRoleRepository
{
    //public override async Task<RoleEntity> CreateAsync(string roleName)
    //{
    //    if (roleName == null)
    //        return null!;

    //    try
    //    {
    //        await _dbSet.AddAsync(roleName);
    //        await _context.SaveChangesAsync();
    //        return roleName;
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine($"Error creating {nameof(RoleEntity)} entity :: {ex.Message}");
    //        return null!;
    //    }
    //}
}