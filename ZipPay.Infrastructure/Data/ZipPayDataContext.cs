using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZipPay.Domain.Entity;

namespace ZipPay.Infrastructure.Data
{
    public class ZipPayDataContext : DbContext
    {
        public ZipPayDataContext()
        {
        }

        public ZipPayDataContext(DbContextOptions<ZipPayDataContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(c => new { c.Email }).IsUnique();
                entity.Property(p => p.RowVersion).IsConcurrencyToken();
            });

           
            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasIndex(c => new { c.Email }).IsUnique();
                entity.Property(p => p.RowVersion).IsConcurrencyToken();
            });
        }

    }
}
