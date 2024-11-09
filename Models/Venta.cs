using System;
using System.Collections.Generic;

namespace PracticasApis.Models;

public partial class Venta
{
    public int DiaId { get; set; }

    public string? Dia { get; set; }

    public int? Cantidad { get; set; }
}
