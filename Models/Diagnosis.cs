using System;
using System.Collections.Generic;

namespace hospitalsCoursework;

public partial class Diagnosis
{
    public int Id { get; set; }

    public string DiagnosisCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string TreatmentMethod { get; set; } = null!;

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
