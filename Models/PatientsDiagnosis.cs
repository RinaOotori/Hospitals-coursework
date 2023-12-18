using System;
using System.Collections.Generic;

namespace hospitalsCoursework;

public partial class PatientsDiagnosis
{
    public int IdPatient { get; set; }

    public int IdDiagnosis { get; set; }

    public virtual Diagnosis IdDiagnosisNavigation { get; set; } = null!;

    public virtual Patient IdPatientNavigation { get; set; } = null!;
}
