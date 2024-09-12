using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SysApiPrueba.Models;

public partial class DbapiContext : DbContext
{
    public DbapiContext()
    {
    }

    public DbapiContext(DbContextOptions<DbapiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Producto> Producto { get; set; }

  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__CATEGORI__A3C02A1098558628");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__PRODUCTO__09889210CC83091E");

            entity.HasOne(d => d.objCategoria).WithMany(p => p.Producto).HasConstraintName("FK_IDCATEGORIA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
