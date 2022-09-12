using DNSocial.Dal.Extensions;
using DNSocial.Domain.Aggregates.UserProfileAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSocial.Dal
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions opt) : base(opt)
        {
        }
        public DbSet<UserProfile> UserProfiles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyAllConfigurations();
            base.OnModelCreating(builder);
        }
    }
}
