using System;
using System.Collections.Generic;

namespace PracticasApis.Models;

public partial class Ordene
{
    public int IdOrden { get; set; }

    public DateOnly FechaOrden { get; set; }

    public decimal Monto { get; set; }

    public int? IdCliente { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }
}
