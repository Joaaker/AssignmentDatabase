﻿using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class ProjectServiceRepository(DataContext context) : BaseRepository<ProjectServiceJunctionEntity>(context), IProjectServiceRepository
{
}