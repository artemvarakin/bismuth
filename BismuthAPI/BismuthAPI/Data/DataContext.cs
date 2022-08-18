using BismuthAPI.Data.Models;

namespace BismuthAPI.Data;

public sealed class DataContext : DbContext {
    public DataContext(DbContextOptions options) : base(options) { }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Issue> Issues { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Project>().Property(p => p.Name).HasMaxLength(30);
        builder.Entity<Project>().Property(p => p.Description).HasMaxLength(200);

        builder.Entity<Issue>().Property(t => t.Title).HasMaxLength(100);
    }
}