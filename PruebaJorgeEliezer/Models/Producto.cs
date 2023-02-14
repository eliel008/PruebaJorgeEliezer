using System;
using System.Collections.Generic;

namespace PruebaJorgeEliezer.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal Precio { get; set; }

    public bool Existencia { get; set; }

    public bool Estado { get; set; }
}
