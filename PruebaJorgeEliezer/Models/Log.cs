using System;
using System.Collections.Generic;

namespace PruebaJorgeEliezer.Models;

public partial class Log
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public string Descripcion { get; set; }

    public decimal Precio { get; set; }

    public bool Existencia { get; set; }

    public int IdProducto { get; set; }

    public string Estatus { get; set; }
}
