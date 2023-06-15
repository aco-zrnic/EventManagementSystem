using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Web.Entities;

namespace EventManagementSystem.Web.Entities
{
    public class EmContext : DbContext
    {
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Sponsor> Sponsors { get; set; }

        public EmContext(DbContextOptions<EmContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participant>(
                entity =>
                {
                    entity.HasMany(p => p.Ticket).WithOne(t => t.Participant);
                }
            );

            modelBuilder.Entity<Event>(
                entity =>
                {
                    entity
                        .HasMany(e => e.Sponsor)
                        .WithOne(s => s.Event)
                        .HasForeignKey(s => s.EventId);

                    entity
                        .HasMany(e => e.Staff)
                        .WithOne(s => s.Event)
                        .HasForeignKey(s => s.EventId);

                    entity
                        .HasMany(e => e.Ticket)
                        .WithOne(t => t.Event)
                        .HasForeignKey(t => t.EventId);
                }
            );

            modelBuilder.Entity<Ticket>(
                entity =>
                {
                    entity
                        .HasOne(t => t.Registration)
                        .WithOne(r => r.Ticket)
                        .HasForeignKey<Registration>(r => r.TicketId);
                }
            );
        }
    }
}
