using Core.Persistence.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;

namespace Kodlama.io.Devs.Persistence.Contexts;

public class KodlamaIoContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
    public DbSet<ProgrammingTechnology> ProgrammingTechnologies { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<GithubProfile> GithubProfiles { get; set; }

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

            t.HasMany(t => t.ProgrammingTechnologies);
        });

        modelBuilder.Entity<ProgrammingTechnology>(t =>
        {
            t.ToTable("ProgrammingTechnologies").HasKey(x => x.Id);
            t.Property(x => x.Id).HasColumnName("Id");
            t.Property(x => x.Name).HasColumnName("Name");
            t.Property(x => x.CreatedDate).HasColumnName("CreatedDate");
            t.Property(x => x.ModifiedDate).HasColumnName("ModifiedName");
            t.Property(x => x.IsDeleted).HasColumnName("IsDeleted");

            t.HasOne(t => t.ProgrammingLanguage);
        });

        ProgrammingLanguage[] programmingTableSeeds = { new(1, "C#"), new(2, "Python"), new(3, "Ruby") };
        modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingTableSeeds);

        ProgrammingTechnology[] programmingTechnologyTableSeed =
        {
            new(1, ".Net Core", 1),
            new(2, "Django", 2),
            new(3, "React.js", 3)
        };
        modelBuilder.Entity<ProgrammingTechnology>().HasData(programmingTechnologyTableSeed);

        modelBuilder.Entity<User>(t =>
        {
            t.ToTable("Users").HasKey(i => i.Id);
            t.Property(a => a.Id).HasColumnName("Id");
            t.Property(a => a.AuthenticatorType).HasColumnName("AuthenticatorType");
            t.Property(a => a.Email).HasColumnName("Email");
            t.Property(a => a.FirstName).HasColumnName("FirstName");
            t.Property(a => a.LastName).HasColumnName("LastName");
            t.Property(a => a.PasswordHash).HasColumnName("PasswordHash");
            t.Property(a => a.PasswordSalt).HasColumnName("PasswordSalt");
            t.Property(a => a.Status).HasColumnName("Status");


            t.HasMany(t => t.RefreshTokens);
            t.HasMany(t => t.UserOperationClaims);
        });

        modelBuilder.Entity<OperationClaim>(t =>
        {
            t.ToTable("OperationClaims").HasKey(i => i.Id);
            t.Property(i => i.Id).HasColumnName("Id");
            t.Property(i => i.Name).HasColumnName("Name");
        });

        OperationClaim[] operationClaimTableSeeds =
               {
            new(1, "Admin"),
            new(2, "Editor")
        };

        modelBuilder.Entity<OperationClaim>().HasData(operationClaimTableSeeds);

        modelBuilder.Entity<UserOperationClaim>(t =>
        {
            t.ToTable("UserOperationClaims").HasKey(i => i.Id);
            t.Property(i => i.Id).HasColumnName("Id");
            t.Property(i => i.UserId).HasColumnName("UserId");
            t.Property(i => i.OperationClaimId).HasColumnName("OperationClaimId");

            t.HasOne(i => i.User);
            t.HasOne(i => i.OperationClaim);
        });

        modelBuilder.Entity<GithubProfile>(t =>
        {
            t.ToTable("GithubProfiles").HasKey(i => i.Id);
            t.Property(i => i.Id).HasColumnName("Id");
            t.Property(i => i.UserId).HasColumnName("UserId");
            t.Property(i => i.GithubProfileUrl).HasColumnName("GithubProfileUrl");

            t.HasOne(i => i.User);
        });
        GithubProfile[] githubProfilesTableSeeds =
        {
            new(1, 1, "https://github.com/SaruGit75")
        };


        modelBuilder.Entity<GithubProfile>().HasData(githubProfilesTableSeeds);


        HashingHelper.CreatePasswordHash("Abc123", out var adminHashedPass, out var adminSaltedPass);
        HashingHelper.CreatePasswordHash("Abc123", out var editorHashedPass, out var editorSaltedPass);

        User[] userTableSeeds =
        {
            new()
            {
                Id = 1,
                AuthenticatorType = AuthenticatorType.None,
                Email = "tst@editor.com",
                FirstName = "Testeditor",
                LastName = "editor",
                PasswordHash = editorHashedPass,
                PasswordSalt = editorSaltedPass,
                Status = true
            },
            new()
            {
                Id = 2,
                AuthenticatorType = AuthenticatorType.Otp,
                Email = "tst@adm.com",
                FirstName = "TestAdm",
                LastName = "admin",
                PasswordHash = adminHashedPass,
                PasswordSalt = adminSaltedPass,
                Status = true
            }
        };
        modelBuilder.Entity<User>().HasData(userTableSeeds);

    }






    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // TODO Generic biçimde nasıl elleçlenir ? Refactor edilecek 
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