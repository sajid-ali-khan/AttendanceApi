using System;
using System.Collections.Generic;

namespace AttendanceApi.OldModels;

public partial class RawStudent
{
    public string Rollno { get; set; } = null!;

    public string Name { get; set; } = null!;

    public byte Branch { get; set; }

    public string Sec { get; set; } = null!;

    public byte Sem { get; set; }
}
