﻿using System;
using System.Collections.Generic;

namespace APIVotos.Models;

public partial class Estadistica
{
    public int? Votos { get; set; }

    public int? Fraudes { get; set; }

    public int Id { get; set; }
}
