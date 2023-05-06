using System;
using System.Collections.Generic;

namespace APIVotos.Models;

public partial class Votante
{
    public string Dpi { get; set; } = null!;

    public string? Nombre { get; set; }

    public bool? EstadoVoto { get; set; }

    public string? Contrasenia { get; set; }

    public virtual ICollection<Voto> Votos { get; set; } = new List<Voto>();
}
