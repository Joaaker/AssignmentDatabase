using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<StatusEntity> Statuses { get; set; }
    public DbSet<UnitTypeEntity> UnitTypes { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<ServiceEntity> Services { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    //Denna kod är genererad av ChatGPT o3-mini - Den skapar 3st Status när man kör migration
        modelBuilder.Entity<StatusEntity>()
            .HasData( 
            new StatusEntity { Id = 1, StatusName = "Ej påbörjat" },
            new StatusEntity { Id = 2, StatusName = "Pågående" },
            new StatusEntity { Id = 3, StatusName = "Avslutat" }
        );

        modelBuilder.Entity<EmployeeEntity>()
            .HasOne(x => x.Role)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ServiceEntity>()
            .HasOne(x => x.UnitType)
            .WithMany(x => x.Services)
            .HasForeignKey(x => x.UnitId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(x => x.Customer)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(x => x.ProjectManager)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.ProjectManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(x => x.Status)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjectEntity>()
            .Property(x => x.Id)
            .UseIdentityColumn(100, 1);

        modelBuilder.Entity<ProjectServiceJunctionEntity>()
            .HasKey(x => new { x.ProjectId, x.ServiceId });

        modelBuilder.Entity<ProjectServiceJunctionEntity>()
            .HasOne(x => x.Project)
            .WithMany(x => x.ProjectServices)
            .HasForeignKey(x => x.ProjectId);

        modelBuilder.Entity<ProjectServiceJunctionEntity>()
            .HasOne(x => x.Service)
            .WithMany(x => x.ProjectServices)
            .HasForeignKey(x => x.ServiceId);
    }
}