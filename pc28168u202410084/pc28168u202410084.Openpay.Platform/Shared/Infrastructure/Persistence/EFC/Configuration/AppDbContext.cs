using pc28168u202410084.Settlements.Domain.Model.Aggregates;
using pc28168u202410084.Settlements.Domain.Model.ValueObjects;
using pc28168u202410084.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using pc28168u202410084.Shared.Infrastructure.Persistence.EFC.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace pc28168u202410084.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Apply audit timestamp interceptor for all IAuditableEntity implementations
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<DisbursementOrder>().HasKey(f => f.Id);
        builder.Entity<DisbursementOrder>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<DisbursementOrder>()
            .Property(f => f.SourceId)
            .HasConversion(valueObject => valueObject.Value, value => new SourceId(value))
            .IsRequired();

        builder.Entity<FavoriteSource>()
            .Property(f => f.NewsApiKey)
            .HasConversion(valueObject => valueObject.Value, value => new NewsApiKey(value))
            .IsRequired();
        builder.Entity<FavoriteSource>()
            .HasIndex(f => new { f.NewsApiKey, f.SourceId })
            .IsUnique();

        builder.UseSnakeCaseNamingConvention();
    }
}