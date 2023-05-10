using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TimeHelper.Data.DbAccess.Models;

namespace TimeHelper.Data.DbAccess.Context;

public partial class TimeHelperContext : DbContext
{
    public TimeHelperContext()
    {
    }

    public TimeHelperContext(DbContextOptions<TimeHelperContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Date> Dates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(Util.ConnectionString.Get());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Date>(entity =>
        {
            entity.ToTable("Date");

            entity.HasIndex(e => e.DateName, "UC_DateName").IsUnique();

            entity.Property(e => e.DateName).HasMaxLength(256);
            entity.Property(e => e.DateValue).HasColumnType("date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
