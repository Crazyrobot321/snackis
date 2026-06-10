using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Snackis.Infrastructure.Data
{
    public class MyDbContext(DbContextOptions<MyDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<ApplicationTopic> Topics { get; set; }
        public DbSet<ApplicationPost> Posts { get; set; }
        public DbSet<ApplicationCategory> Categories { get; set; }
        public DbSet<ApplicationSubCategory> SubCategories { get; set; }
        public DbSet<ApplicationPrivateMessage> privateMessages { get; set; }
        public DbSet<ApplicationReport> UserReports { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //prevent cascade delete, all message will be deleted otherwise
            builder.Entity<ApplicationPrivateMessage>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.NoAction);
            //same here but for receiver
            builder.Entity<ApplicationPrivateMessage>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);
            //Links the reporter of a report to the user, .include wasn't enough
            builder.Entity<ApplicationReport>()
                .HasOne(r => r.Reporter)
                .WithMany()
                .HasForeignKey(r => r.ReporterUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
