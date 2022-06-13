using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Core.Repository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Film> Films { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>(b => b.Property(c => c.Id).HasDefaultValueSql("newsequentialid()"));
            modelBuilder.Entity<Genre>(b => b.Property(g => g.Id).HasDefaultValueSql("newsequentialid()"));
            modelBuilder.Entity<Film>(b => b.Property(f => f.Id).HasDefaultValueSql("newsequentialid()"));

            modelBuilder.Entity<Rating>(rating =>
            {
                rating.HasKey(r => new { r.PersonId, r.FilmId });
                rating.HasOne(r => r.ApplicationUser)
                    .WithMany(u => u.Ratings)
                    .HasForeignKey(r => r.PersonId)
                    .IsRequired();
                rating.HasOne(r => r.Film)
                    .WithMany(f => f.Ratings)
                    .HasForeignKey(r => r.FilmId)
                    .IsRequired();
            });

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            Country[] countries =
            {
                new Country()
                {
                    Id = new Guid("615A250D-596B-4862-B095-72CAFE98401E"),
                    Name = "Ukraine"
                },
                new Country()
                {
                    Id = new Guid("515A250D-596B-4862-B095-72CAFE98401E"),
                    Name = "USA"
                },
                new Country()
                {
                    Id = new Guid("415A250D-596B-4862-B095-72CAFE98401E"),
                    Name = "Poland"
                }
            };

            Film[] films =
            {
                new Film()
                {
                    Id = new Guid("615A250D-596B-4862-B095-72CAFE98401E"),
                    Name = "Spider man",
                    Description = "my description",
                    ReleaseDate = DateTime.Now
                },
                new Film()
                {
                    Id = new Guid("715A250D-596B-4862-B095-72CAFE98401E"),
                    Name = "Film#2",
                    Description = "my description very long description afsdfasdfasdkljfkajsdhf ljkasd ljkhcxvl kl;kj fsdgljksdf;lkjfasdg;ljhk sdfgl;kfsdgl;hsdfj;klhg;l sfdkg;lk jsdf;lkg dfjgs;ldkf g sdfhgsdfjhgjksdhfkljghsdj fsd fkjghskldjfg",
                    ReleaseDate = DateTime.Now
                },
                new Film()
                {
                    Id = new Guid("515A250D-596B-4862-B095-72CAFE98401E"),
                    Name = "Film#1",
                    Description = "my description#1",
                    ReleaseDate = DateTime.Now
                }
            };

            modelBuilder.Entity<Country>().HasData(countries);
            modelBuilder.Entity<Film>().HasData(films);

            IdentityRole[] roles = {
                new IdentityRole()
                {
                    Id = "EDD8E58E-C34F-4CE8-B910-41A85389D143",
                    Name = "System Admin",
                    NormalizedName = "SYSTEM ADMIN"
                },
                new IdentityRole()
                {
                    Id = "2867822E-73C5-4931-A18C-206122114A2A",
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            var user = new ApplicationUser()
            {
                Id = "A5CD247E-2647-42E8-97B1-EAC777D33684",
                UserName = "sysAdmin@mail.com",
                NormalizedUserName = "SYSADMIN@MAIL.COM",
                Email = "sysAdmin@mail.com",
                NormalizedEmail = "SYSADMIN@MAIL.COM",
                SecurityStamp = "RCVAV4G7POXXPORBFV4MJA5CF6FK6DWC",
                PasswordHash = "AQAAAAEAACcQAAAAEEBqf0+Sb/mN1DTq5tqzbEnTjCg26GaSOosFHIeF6R4NyymKfbcKPJx+JcBL120Vpw=="
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
            modelBuilder.Entity<ApplicationUser>().HasData(user);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>()
            {
                UserId = "A5CD247E-2647-42E8-97B1-EAC777D33684",
                RoleId = "EDD8E58E-C34F-4CE8-B910-41A85389D143"
            });
        }
    }
}
