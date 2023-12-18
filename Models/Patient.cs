using System;
using System.Collections.Generic;

namespace hospitalsCoursework;

public partial class Patient
{
    public int Id { get; set; }

    public string HospitalCode { get; set; } = null!;

    public string DoctorTin { get; set; } = null!;

    public string DiagnosisCode { get; set; } = null!;

    public DateOnly HospitalizationDate { get; set; }

    public DateOnly ExtractDate { get; set; }

    public string Name { get; set; } = null!;

    public string Tin { get; set; } = null!;

    public string ConditionDischarge { get; set; } = null!;

    public virtual Diagnosis DiagnosisCodeNavigation { get; set; } = null!;

    public virtual Doctor DoctorTinNavigation { get; set; } = null!;

    public virtual Hospital HospitalCodeNavigation { get; set; } = null!;
}
