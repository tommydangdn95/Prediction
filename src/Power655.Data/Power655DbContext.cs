using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Power655.Data;

public sealed class Power655DbContext(DbContextOptions<Power655DbContext> options) : DbContext(options)
{
    public DbSet<DrawEntity> Draws => Set<DrawEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var draw = modelBuilder.Entity<DrawEntity>();
        draw.ToTable("Draws");
        draw.HasKey(d => new { d.Game, d.DrawId });
        draw.Property(d => d.Game).HasMaxLength(8).IsRequired();
        draw.Property(d => d.DrawId).ValueGeneratedNever();
        draw.Property(d => d.Source).HasMaxLength(32).IsRequired();
        draw.HasIndex(d => new { d.Game, d.Date });
    }

    public static DbContextOptions<Power655DbContext> CreateOptions(string databasePath) =>
        new DbContextOptionsBuilder<Power655DbContext>()
            .UseSqlite($"Data Source={databasePath}")
            .Options;
}

/// <summary>Dùng bởi <c>dotnet ef migrations</c>.</summary>
public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Power655DbContext>
{
    public Power655DbContext CreateDbContext(string[] args) =>
        new(Power655DbContext.CreateOptions("power655.db"));
}
