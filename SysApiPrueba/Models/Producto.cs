﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Cors;

namespace SysApiPrueba.Models;

[Table("PRODUCTO")]
public partial class Producto
{
    [Key]
    public int IdProducto { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? CodigoBarra { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Marca { get; set; }

    public int? IdCategoria { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Precio { get; set; }

    [ForeignKey("IdCategoria")]
    [InverseProperty("Producto")]
    public virtual Categoria? objCategoria { get; set; }
}