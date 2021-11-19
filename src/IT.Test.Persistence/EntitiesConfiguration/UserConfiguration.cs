// Тестовое задание https://github.com/boiledgas/IT.Test

using IT.Test.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IT.Test.Model.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("name");
            builder.Property(e => e.Surname)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("surname");
            builder.Property(e => e.Patronymic)
                .HasColumnType("varchar(255)")
                .HasColumnName("patronymic");
            builder.Property(e => e.Number)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("number");
            builder.Property(e => e.Email)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("email")
                .UseCollation("nocase");
            builder.HasIndex(e => e.Email)
                .IsUnique();

            builder
                .HasOne(e => e.Organization)
                .WithMany(o => o.Users);
            builder.Navigation(e => e.Organization).AutoInclude();

            builder.HasData(
                new User(1, "Иван", "Иванов", "Иванович", "Н-1", "test@mail.ru"),
                new User(2, "Петр", "Петров", "Петрович", "Н-2", "test@gmail.ru"));
        }
    }
}
