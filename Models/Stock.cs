using System;
using System.Collections.Generic;

namespace PracticasApis.Models;

public partial class Stock
{
    public int Codigo { get; set; }

    public string? Nombre { get; set; }

    public int? CodigoCategoria { get; set; }

    public int? CodigoProveedor { get; set; }

    public virtual Categorium? CodigoCategoriaNavigation { get; set; }

    public virtual Proveedor? CodigoProveedorNavigation { get; set; }
}
