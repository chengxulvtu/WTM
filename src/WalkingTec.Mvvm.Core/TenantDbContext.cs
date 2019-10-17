using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace WalkingTec.Mvvm.Core
{
    //public class TenantDbContext : DbContext
    //{
    //    public DbSet<Tenant> Tenants { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        var path = Assembly.GetEntryAssembly().Location;
    //        var dir = new DirectoryInfo(Path.GetDirectoryName(path));

    //        var tenanbDbPath = Path.Combine(dir.FullName, "tenant.db");

    //        optionsBuilder.UseSqlite($"Data Source={tenanbDbPath}");
    //        base.OnConfiguring(optionsBuilder);
    //    }

    //}
}
