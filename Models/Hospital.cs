using System;
using System.Collections.Generic;

namespace hospitalsCoursework;

public partial class Hospital
{
    public int Id { get; set; }

    public string HospitalCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Tin { get; set; } = null!;

    public string AgePatients { get; set; } = null!;

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
