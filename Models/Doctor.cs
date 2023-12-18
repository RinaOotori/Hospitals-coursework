using System;
using System.Collections.Generic;

namespace hospitalsCoursework;

public partial class Doctor
{
    public int Id { get; set; }

    public string HospitalCode { get; set; } = null!;

    public string DepartmentCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Tin { get; set; } = null!;

    public string PostCode { get; set; } = null!;

    public virtual Department DepartmentCodeNavigation { get; set; } = null!;

    public virtual Hospital HospitalCodeNavigation { get; set; } = null!;

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual Post PostCodeNavigation { get; set; } = null!;
}
