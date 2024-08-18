    using BrightStarPhase2AssessmentTestEventManagement.Models;
    using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Reflection.Emit;

namespace BrightStarPhase2AssessmentTestEventManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Additional configurations can be added here
            builder.Entity<Event>()
                .HasMany(e => e.Participants)
                .WithOne(p => p.Event)
                .HasForeignKey(p => p.EventId);

            builder.Entity<Participant>()
                .HasKey(p => new { p.EventId, p.UserId });
        }
    }

}
