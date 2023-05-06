using System;
using System.Collections.Generic;

namespace IngeSoftFinal.Models;

public partial class Voto
{
    public string VotanteDpi { get; set; } = null!;

    public int CandidatoId { get; set; }

    public DateTime? FechaHora { get; set; }

    public string? IpCompu { get; set; }

    public virtual Candidato Candidato { get; set; } = null!;

    public virtual Votante VotanteDpiNavigation { get; set; } = null!;
}
