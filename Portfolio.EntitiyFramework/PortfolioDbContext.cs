using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Entities.Account;
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


