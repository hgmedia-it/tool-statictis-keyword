using tool_statictis_keyword.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tool_statictis_keyword.Models.Data;
using Microsoft.AspNetCore.Identity;

namespace tool_statictis_keyword.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public string UserId { get; set; }
        public virtual DbSet<Keyword> Keyword { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Date> Date { get; set; }
        public virtual DbSet<Statictis> Statictis { get; set; }
        public virtual DbSet<Video> Video { get; set; }
        public virtual DbSet<SearchByDayResult> SearchByDayResults { get; set; }
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ConfigRelationShip(builder);
            SeedData(builder);
        }
        private void ConfigRelationShip(ModelBuilder builder)
        {
            //Keyword
            builder.Entity<Keyword>().HasKey(ic => new { ic.Name });
            builder.Entity<Keyword>().Property(ic => ic.Id).ValueGeneratedOnAdd();

            //Category
            builder.Entity<Category>().HasKey(ic => new { ic.CategoryName });
            builder.Entity<Category>().Property(ic => ic.Id).ValueGeneratedOnAdd();

            //Date
            builder.Entity<Date>().HasKey(ic => new { ic.Id });
            builder.Entity<Date>().Property(ic => ic.Id).ValueGeneratedOnAdd();

            builder.Entity<SearchByDayResult>().HasKey(ic => new { ic.Id });
            builder.Entity<SearchByDayResult>().Property(ic => ic.Id).ValueGeneratedOnAdd();

            builder.Entity<Video>().HasKey(ic => new { ic.VideoId });
            builder.Entity<Video>().Property(ic => ic.Id).ValueGeneratedOnAdd();

            //Statictis
            builder.Entity<Statictis>().HasKey(ic => new { ic.KeywordId, ic.DateId });

        }
        private void SeedData(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole[]
            {
                new IdentityRole{Id = "ec864316-078a-406e-9013-f5e3d20d1f88", Name = "admin", NormalizedName="ADMIN"},
                new IdentityRole{Id = "ec864316-078a-406e-9013-f5e3d20d1f89", Name = "manager", NormalizedName="MANAGER"},
                new IdentityRole{Id = "ec864316-078a-406e-9013-f5e3d20d1f90", Name = "staff", NormalizedName="STAFF"}
            });

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser[]
            {
                new ApplicationUser{Id = "9cc0bd9c-d309-4b1a-b124-c9e915bbec23", UserName = "admin", NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com", NormalizedEmail = "ADMIN@GMAIL.COM",EmailConfirmed = false, FullName = "Ad",
                    PasswordHash = "AQAAAAEAACcQAAAAEHpP+NNw1Kb2HeY/mDctx6M0EY60IEgyfupwny8MQFOElVF1dCpvDN8mAj2Oig7bCQ==",
                    SecurityStamp = "RP4A4QYMVOZPLECVZJHL4FUEKK3KJAE2", ConcurrencyStamp = "203d261d-e635-4ded-8310-2060366d61c2",
                    PhoneNumber = "0359038319", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0}
            });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "ec864316-078a-406e-9013-f5e3d20d1f88", UserId = "9cc0bd9c-d309-4b1a-b124-c9e915bbec23" });
        }
    }
}
