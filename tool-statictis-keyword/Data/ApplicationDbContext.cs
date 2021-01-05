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
        public virtual DbSet<Video> Video { get; set; }
        public virtual DbSet<Campaign> Campaign { get; set; }
        public virtual DbSet<ViewsCountByDay> ViewsCountByDay { get; set; }
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ConfigRelationShip(builder);
            ConfigGlobalFilter(builder);
            SeedData(builder);
        }
        private void ConfigRelationShip(ModelBuilder builder)
        {
            
            builder.Entity<Keyword_Video>().HasKey(ic => new { ic.KeywordId, ic.VideoId });
            builder.Entity<Keyword_Video>()
                .HasOne<Keyword>(ic => ic.Keyword)
                .WithMany(c => c.Videos)
                .HasForeignKey(ic => ic.KeywordId);
            builder.Entity<Keyword_Video>()
                .HasOne<Video>(ic => ic.Video)
                .WithMany(i => i.Keywords)
                .HasForeignKey(ic => ic.VideoId);
        }
        private void ConfigGlobalFilter(ModelBuilder builder)
        {
            builder.Entity<Video>().HasQueryFilter(i => string.IsNullOrEmpty(UserId) || i.UserId == UserId);
            builder.Entity<Keyword>().HasQueryFilter(i => string.IsNullOrEmpty(UserId) || i.UserId == UserId);
            builder.Entity<Campaign>().HasQueryFilter(i => string.IsNullOrEmpty(UserId) || i.UserId == UserId);
            builder.Entity<ViewsCountByDay>().HasQueryFilter(i => string.IsNullOrEmpty(UserId) || i.UserId == UserId);
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
                new ApplicationUser{Id = "92ad4f43-4be7-4fb8-909f-ced532c58461", UserName = "admin", NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com", NormalizedEmail = "ADMIN@GMAIL.COM",EmailConfirmed = true, FullName = "Ad",
                    PasswordHash = "AQAAAAEAACcQAAAAEDjtxkKmq1HdWDIebT4dygGnXFswCLC1irkWgM0FQ3K5mCnKLFfLYRmA8Q6W9r+z4w==",
                    SecurityStamp = "64QW72XRQWP5FI2IWOZV3ZD6ILSBV4W2", ConcurrencyStamp = "90016888-7668-4a47-9050-de70b8aa621b",
                    PhoneNumber = "0359038319", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0}
            });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "ec864316-078a-406e-9013-f5e3d20d1f88", UserId = "92ad4f43-4be7-4fb8-909f-ced532c58461" });
        }
    }
}
