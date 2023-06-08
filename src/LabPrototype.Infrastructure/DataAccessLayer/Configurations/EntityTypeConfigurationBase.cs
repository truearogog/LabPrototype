using LabPrototype.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPrototype.Infrastructure.DataAccessLayer.Configurations
{
    public abstract class EntityTypeConfigurationBase<T> : IEntityTypeConfigurationBase, IEntityTypeConfiguration<T> where T : EntityBase
    {
        public void Configure(ModelBuilder builder) => Configure(builder.Entity<T>());

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
