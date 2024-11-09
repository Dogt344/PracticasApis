using System;
using System.Collections.Generic;

namespace PracticasApis.Models;

public partial class Categorium
{
    public int CodigoCategoria { get; set; }

    public string? NombreCategoria { get; set; }

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
