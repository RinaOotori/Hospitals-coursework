using System;
using System.Collections.Generic;

namespace hospitalsCoursework;

public partial class PatientsDoctor
{
    public int IdPatient { get; set; }

    public int IdDoctor { get; set; }

    public virtual Doctor IdDoctorNavigation { get; set; } = null!;

    public virtual Patient IdPatientNavigation { get; set; } = null!;
}
