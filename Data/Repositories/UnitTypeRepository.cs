using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class UnitTypeRepository(DataContext context) : BaseRepository<UnitTypeEntity>(context), IUnitTypeRepository
{

}