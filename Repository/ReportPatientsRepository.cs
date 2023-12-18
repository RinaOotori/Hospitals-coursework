namespace hospitalsCoursework.Repository;

public class ReportPatientsRepository : BaseRepository
{
    protected (List<Patient> patients, Doctor? doctor) GetPatientsByDoctorTin(DateOnly firstDate, DateOnly secondDate, string doctorTin)
    {
        var doctor = new DoctorsRepository().ShowTable(new List<string>(), new List<string>(), new List<string>(),
            new List<string>{doctorTin}, new List<string>()).FirstOrDefault();
        var patients = Db.Patients.Where(p =>
            p.HospitalizationDate >= firstDate && p.ExtractDate <= secondDate && p.DoctorTin == doctorTin).ToList();
        return (patients, doctor);
    }
}