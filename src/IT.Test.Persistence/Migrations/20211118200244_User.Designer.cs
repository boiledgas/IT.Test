﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IT.Test.Persistence.Migrations
{
    [DbContext(typeof(PersistenceContext))]
    [Migration("20211118200244_User")]
    partial class User
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("IT.Test.Persistence.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("number");

                    b.Property<string>("Patronymic")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("patronymic");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("surname");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "test@mail.ru",
                            Name = "Иван",
                            Number = "Н-1",
                            Patronymic = "Иванович",
                            Surname = "Иванов"
                        },
                        new
                        {
                            Id = 2,
                            Email = "test@gmail.ru",
                            Name = "Петр",
                            Number = "Н-2",
                            Patronymic = "Петрович",
                            Surname = "Петров"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
