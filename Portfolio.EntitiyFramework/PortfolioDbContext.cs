using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Entities.Account;
using Portfolio.Core.Entities.Job;
using Portfolio.Core.Entities.Personal;
using Portfolio.Core.Entities.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure;

public class PortfolioDbContext : DbContext
{

    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
        : base(options)
    
    {
        this.Database.SetCommandTimeout(30);

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserLoginAttempt> UserLoginAttemptds { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Personal> Personals { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<JobExperience> JobExperiences { get; set; }

    #region on model creating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var realtionShip in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
        {
            realtionShip.DeleteBehavior = DeleteBehavior.Restrict;
        }

        base.OnModelCreating(modelBuilder);
    }
    #endregion
}


