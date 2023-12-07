using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reisinger_Intelliface_1_0.Model;

namespace Reisinger_Intelliface_1_0.Data;

public class IntellifaceDataContext : IdentityDbContext
{
    private readonly DbContextOptions<IntellifaceDataContext> _contextOptions;
    protected readonly IConfiguration Configuration;

    public IntellifaceDataContext(DbContextOptions<IntellifaceDataContext> contextOptions, IConfiguration configuration)
        : base(contextOptions)
    {
        _contextOptions = contextOptions;
        Configuration = configuration;
    }

    public DbSet<User> Employees { get; set; }
    public DbSet<FaceImage> FaceImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
        modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
        modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();


        modelBuilder.Entity<User>()
            .HasKey(e => e.ID);

        modelBuilder.Entity<FaceImage>()
            .HasKey(fi => fi.ID);


        modelBuilder.Entity<FaceImage>()
            .HasOne(fi => fi.Employee)
            .WithMany(e => e.Images)
            .HasForeignKey(fi => fi.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);




    }


}