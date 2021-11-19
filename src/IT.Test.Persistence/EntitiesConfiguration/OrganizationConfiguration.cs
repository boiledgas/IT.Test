// Тестовое задание https://github.com/boiledgas/IT.Test

using IT.Test.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IT.Test.Model.EntitiesConfiguration
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("name")
                .UseCollation("nocase");

            builder.HasIndex(e => e.Name)
                .IsUnique();

            builder
                .HasMany(e => e.Users)
                .WithOne(o => o.Organization);

            builder.HasData(
                new Organization(1, "Информационные технологии"));
        }
    }
}
