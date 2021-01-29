using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TesteDotNet2.ProjectControlSystem.Data.SqlServer.Extension
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}
