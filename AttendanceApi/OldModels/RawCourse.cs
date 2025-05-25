using System;
using System.Collections.Generic;

namespace AttendanceApi.OldModels;

public partial class RawCourse
{
    public string Degr { get; set; } = null!;

    public string Scheme { get; set; } = null!;

    public byte Branch { get; set; }

    public byte Sem { get; set; }

    public string Scode { get; set; } = null!;

    public string Subname { get; set; } = null!;
}
