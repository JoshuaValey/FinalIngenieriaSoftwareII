using System;
using System.Collections.Generic;

namespace APIVotos.Models;

public partial class Candidato
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? Partido { get; set; }

    public virtual ICollection<Voto> Votos { get; set; } = new List<Voto>();
}
