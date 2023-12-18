using System;
using System.Collections.Generic;

namespace hospitalsCoursework;

public partial class HospitalsDepartment
{
    public int IdHospital { get; set; }

    public int IdDepartment { get; set; }

    public virtual Department IdDepartmentNavigation { get; set; } = null!;

    public virtual Hospital IdHospitalNavigation { get; set; } = null!;
}
