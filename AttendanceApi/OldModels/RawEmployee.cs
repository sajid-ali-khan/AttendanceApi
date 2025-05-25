using System;
using System.Collections.Generic;

namespace AttendanceApi.OldModels;

public partial class RawEmployee
{
    public short Empid { get; set; }

    public short Pwd { get; set; }

    public string? Gender { get; set; }

    public string? Salu { get; set; }

    public string? Name { get; set; }
}
