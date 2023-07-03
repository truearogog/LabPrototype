﻿using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LabPrototype.Infrastructure.DataAccessLayer.Repositories
{
    public class MeterTypeMeasurementTypeRepository : RepositoryBase<MeterTypeMeasurementTypeEntity>, IMeterTypeMeasurementTypeRepository
    {
        public MeterTypeMeasurementTypeRepository(LabDbContext dbContext) : base(dbContext, dbContext.MeterTypeMeasurementTypes)
        {
        }
    }
}
