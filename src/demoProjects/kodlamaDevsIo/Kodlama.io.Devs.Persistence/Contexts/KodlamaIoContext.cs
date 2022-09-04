using Core.Persistence.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Kodlama.io.Devs.Persistence.Contexts;

public class KodlamaIoContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

    public KodlamaIoContext(DbContextOptions<KodlamaIoContext> options, IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgrammingLanguage>(t =>
        {
            t.ToTable("ProgrammingLanguages").HasKey(x => x.Id);
            t.Property(x => x.Id).HasColumnName("Id");
            t.Property(x => x.Name).HasColumnName("Name");
            t.Property(x => x.CreatedDate).HasColumnName("CreatedDate");
            t.Property(x => x.ModifiedDate).HasColumnName("ModifiedName");
            t.Property(x => x.IsDeleted).HasColumnName("IsDeleted");
        });

        ProgrammingLanguage[] programmingTableSeeds = { new(1, "C#"), new(2, "Python"), new(3, "Ruby") };
        modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingTableSeeds);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var datas = ChangeTracker.Entries<ProgrammingLanguage>().ToList();

        datas.ForEach(data =>
        {
            _ = data.State switch
            {
                EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                EntityState.Modified => data.Entity.ModifiedDate = DateTime.Now,
                _ => DateTime.Now
            };
        });
        return await base.SaveChangesAsync(cancellationToken);
    }
}