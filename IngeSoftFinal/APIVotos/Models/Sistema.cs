﻿using System;
using System.Collections.Generic;

namespace APIVotos.Models;

public partial class Sistema
{
    public int Id { get; set; }

    public string? Fase { get; set; }

    public bool? Vigente { get; set; }
}
