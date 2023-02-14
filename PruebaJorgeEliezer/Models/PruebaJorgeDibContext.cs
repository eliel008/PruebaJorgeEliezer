using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PruebaJorgeEliezer.Models;

public partial class PruebaJorgeDibContext : DbContext
{
    public PruebaJorgeDibContext()
    {
    }

    public PruebaJorgeDibContext(DbContextOptions<PruebaJorgeDibContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=PruebaJorgeDib; integrated security=true;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>(entity =>
        {
            entity.Property(e => e.Descripcion).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.Property(e => e.Descripcion).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
