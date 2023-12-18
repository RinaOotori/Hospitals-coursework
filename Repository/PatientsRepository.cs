using Microsoft.EntityFrameworkCore;

namespace hospitalsCoursework.Repository;

public class PatientsRepository : BaseRepository
{
    public List<Patient> ShowTable(List<string> hospitalCodeFilter, List<string> doctorTinFilter, List<string> nameFilter,
        List<string> diagnosisCodeFilter, List<DateOnly> hospitalizationDateFilter, List<DateOnly> extractDateFilter,
        List<string> tinFilter, List<string> conditionDischargeFilter)
    {
        var dbSet = this.Db.Patients;
        var result = dbSet.ToList();
        if (hospitalCodeFilter.Count > 0 && !hospitalCodeFilter.Contains("Все"))
        {
            result = result.Where(x => hospitalCodeFilter.Contains(x.HospitalCode)).ToList();
        }

        if (doctorTinFilter.Count > 0 && !doctorTinFilter.Contains("Все"))
        {
            result = result.Where(x => doctorTinFilter.Contains(x.DoctorTin)).ToList();
        }

        if (nameFilter.Count > 0 && !nameFilter.Contains("Все"))
        {
            result = result.Where(x => nameFilter.Contains(x.Name)).ToList();
        }

        if (diagnosisCodeFilter.Count > 0 && !diagnosisCodeFilter.Contains("Все"))
        {
            result = result.Where(x => diagnosisCodeFilter.Contains(x.DiagnosisCode)).ToList();
        }

        if (hospitalizationDateFilter.Count > 0)
        {
            result = result.Where(x => hospitalizationDateFilter.Contains(x.HospitalizationDate)).ToList();
        }

        if (extractDateFilter.Count > 0)
        {
            result = result.Where(x => extractDateFilter.Contains(x.ExtractDate)).ToList();
        }

        if (tinFilter.Count > 0 && !tinFilter.Contains("Все"))
        {
            result = result.Where(x => tinFilter.Contains(x.Tin)).ToList();
        }

        if (conditionDischargeFilter.Count > 0 && !conditionDischargeFilter.Contains("Все"))
        {
            result = result.Where(x => conditionDischargeFilter.Contains(x.ConditionDischarge)).ToList();
        }

        return result;
    }

    public void AddItem(string hospitalCode, string doctorTin, string name, string diagnosisCode,
        DateOnly hospitalizationDate, DateOnly extractDate, string tin, string conditionDischarge)
    {
        var newItem = new Patient
        {
            HospitalCode = hospitalCode, DiagnosisCode = diagnosisCode, HospitalizationDate = hospitalizationDate,
            ExtractDate = extractDate, Tin = tin, Name = name, DoctorTin = doctorTin,
            ConditionDischarge = conditionDischarge
        };
        {
            Db.Patients.Add(newItem);
            Db.SaveChanges();
        }
    }

    public void UpdateItem(Patient patient, string newHospitalCode, string newDoctorTin, string newDiagnosisCode,
        DateOnly newHospitalisationDate, DateOnly newExtractDate, string newName, string newTin,
        string newConditionDischarge)
    {
        {
            Db.Patients
                .Where(x => x.Id == patient.Id)
                .ExecuteUpdate(x => x
                    .SetProperty(p => p.HospitalCode, p => newHospitalCode)
                    .SetProperty(p => p.DoctorTin, p => newDoctorTin)
                    .SetProperty(p => p.DiagnosisCode, p => newDiagnosisCode)
                    .SetProperty(p => p.HospitalizationDate, p => newHospitalisationDate)
                    .SetProperty(p => p.ExtractDate, p => newExtractDate)
                    .SetProperty(p => p.Tin, p => newTin)
                    .SetProperty(p => p.ConditionDischarge, p => newConditionDischarge)
                    .SetProperty(p => p.Name, p => newName));
            Db.SaveChanges();
        }
    }

    public void RemoveItem(string id)
    {
        Db.Patients.Remove(GetById(id));
        Db.SaveChanges();
    }

    private Patient? FindById(string id)
    {
        return Db.Patients.FirstOrDefault(x => x.Id == int.Parse(id));
    }

    private Patient GetById(string id)
    {
        var patient = FindById(id);
        if (patient == null)
        {
            throw new NullReferenceException();
        }

        return patient;
    }
}