using System;
using System.Collections.Generic;

namespace PracticasApis.Models;

public partial class Proveedor
{
    public int CodigoProveedor { get; set; }

    public string? NombreProveedor { get; set; }

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
