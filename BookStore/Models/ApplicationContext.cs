using BookStore.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public virtual DbSet<UserInfo> UserInfos {get;set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasOne(ui => ui.UserInfo)
                .WithOne(u => u.User)
                .HasForeignKey<UserInfo>();
            base.OnModelCreating(builder);
        }
    }
}
