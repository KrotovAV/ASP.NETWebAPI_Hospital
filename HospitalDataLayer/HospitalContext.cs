using HospitalDataLayer.Entityes;
using HospitalDataLayer.Entityes.Base;
using HospitalDataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace HospitalDataLayer
{
    public class HospitalContext : DbContext
    {
        //dotnet ef migrations add InitialMigration
        //dotnet ef database update
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions<HospitalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cabinet> Cabinets { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Specialization> Specializations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();

            optionsBuilder.UseLazyLoadingProxies().
                    UseSqlServer(config.GetConnectionString("Connection"));
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-IFVARIJ\\SQLEXPRESS;Database=HospitalDB;User Id=Admin;Password=MsSQLavk;TrustServerCertificate=True");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Cabinet>(entity =>
            {
                entity.ToTable("Cabinet");
                entity.HasKey(e => e.Id).HasName("PK_Cabinet");
                entity.Property(p => p.Num).IsRequired();
            });

            modelBuilder.Entity<Cabinet>().HasData(
                new Cabinet() { Id = 1, Num = 11 },
                new Cabinet() { Id = 2, Num = 12 },
                new Cabinet() { Id = 3, Num = 13 },
                new Cabinet() { Id = 4, Num = 21 },
                new Cabinet() { Id = 5, Num = 22 },
                new Cabinet() { Id = 6, Num = 23 },
                new Cabinet() { Id = 7, Num = 24 },
                new Cabinet() { Id = 8, Num = 25 }
            );

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("District");
                entity.HasKey(e => e.Id).HasName("PK_District");
                entity.Property(p => p.Num).IsRequired();
            });

            modelBuilder.Entity<District>().HasData(
                new District() { Id = 1, Num = 1},
                new District() { Id = 2, Num = 2},
                new District() { Id = 3, Num = 3}
            );

            modelBuilder.Entity<Specialization>(entity =>
            {
                entity.ToTable("Specialization");
                entity.HasKey(e => e.Id).HasName("PK_Specialization");
                entity.Property(p => p.Name).IsRequired();
            });

            modelBuilder.Entity<Specialization>().HasData(
               new Specialization() { Id = 1, Name = "Педиатр" },
               new Specialization() { Id = 2, Name = "Кардиолог" },
               new Specialization() { Id = 3, Name = "Хирург" },
               new Specialization() { Id = 4, Name = "Окулист" },
               new Specialization() { Id = 5, Name = "Терапевт" }
           );


            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");
                entity.HasKey(e => e.Id).HasName("PK_Doctor");
                entity.Property(e => e.FullName).HasMaxLength(100);
                entity.HasOne(d => d.Cabinet).WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.CabinetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctor_Cabinet");
                entity.HasOne(d => d.District).WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctor_District")
                    .IsRequired(false);
                entity.HasOne(d => d.Specialization).WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.SpecializationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctor_Specialization");
            });

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor() { Id = 1, FullName = "Сергеев Сергей Сергеевич", CabinetId = 1, DistrictId = 1, SpecializationId = 1},
                new Doctor() { Id = 2, FullName = "Иванов Иван Иванович", CabinetId = 2, DistrictId = 1, SpecializationId = 2},
                new Doctor() { Id = 3, FullName = "Дмитриев Дмитрий Дмитриевич", CabinetId = 3, DistrictId = 1, SpecializationId = 3},
                new Doctor() { Id = 4, FullName = "Александров Александр Александрович", CabinetId = 4, DistrictId = 1, SpecializationId = 4},
                new Doctor() { Id = 5, FullName = "Михайлов Михаил Михайлович", CabinetId = 5, DistrictId = 2, SpecializationId = 5},
                new Doctor() { Id = 6, FullName = "Романов Роман Романович", CabinetId = 6, DistrictId = 2, SpecializationId = 5},
                new Doctor() { Id = 7, FullName = "Викторов Виктор Викторович", CabinetId = 7, DistrictId = 3, SpecializationId = 1}
            );

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient");
                entity.HasKey(e => e.Id).HasName("PK_Patient");
                entity.Property(e => e.Address).HasMaxLength(100);
                entity.Property(e => e.BirthdayDate).HasColumnType("datetime");
                entity.Property(e => e.Name).HasMaxLength(30);
                entity.Property(e => e.Patronymic).HasMaxLength(30);
                entity.Property(e => e.Surname).HasMaxLength(30);
                entity.HasOne(d => d.District).WithMany(p => p.Patients)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient_District");
            });

            modelBuilder.Entity<Patient>().HasData(
                new Patient() { Id = 1, Name = "Петр", Surname = "Петров", Patronymic = "Петрович", Address = "Тенистая, д.11, кв.1", BirthdayDate = DateTime.Parse("1991-01-21"), DistrictId = 1, Gender = true },
                new Patient() { Id = 2, Name = "Ольгович", Surname = "Ольга", Patronymic = "Олеговна", Address = "Тенистая, д.22, кв.2", BirthdayDate = DateTime.Parse("1992-02-22"), DistrictId = 1, Gender = false},
                new Patient() { Id = 3, Name = "Пашутов", Surname = "Павел", Patronymic = "Павлович", Address = "Горная, д.33, кв.3", BirthdayDate = DateTime.Parse("1993-03-23"), DistrictId = 2, Gender = true },
                new Patient() { Id = 4, Name = "Васильев", Surname = "Василий", Patronymic = "Васильевич", Address = "Горная, д.44, кв.4", BirthdayDate = DateTime.Parse("1994-04-24"), DistrictId = 2, Gender = true },
                new Patient() { Id = 5, Name = "Антонов", Surname = "Антон", Patronymic = "Антонович", Address = "Васильковая, д.55, кв.5", BirthdayDate = DateTime.Parse("1995-05-25"), DistrictId = 3, Gender = true },
                new Patient() { Id = 6, Name = "Геннадьев", Surname = "Геннадий", Patronymic = "Геннадьевич", Address = "Васильковая, д.66, кв.6", BirthdayDate = DateTime.Parse("1996-06-26"), DistrictId = 3, Gender = true },
                new Patient() { Id = 7, Name = "Александрова", Surname = "Александра", Patronymic = "Александровна", Address = "Васильковая, д.77, кв.7", BirthdayDate = DateTime.Parse("1997-07-27"), DistrictId = 3, Gender = false }
            );
        }
    }
}
