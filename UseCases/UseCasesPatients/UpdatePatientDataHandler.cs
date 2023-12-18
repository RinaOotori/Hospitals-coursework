using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCases.UseCasesPatients;

public class UpdatePatientDataHandler
{
    public void Handle(Patient patient, string newHospitalCode, string newDoctorTin, string newDiagnosisCode,
        DateOnly newHospitalizationDate,
        DateOnly newExtractDate, string newName, string newTin, string newConditionDischarge)
    {
        if (newTin.Contains(" ") || newTin.Length != 10)
        {
            throw new FormatException("ИНН должен состоять из 10 символов");
        }
        
        if (newConditionDischarge.Length < 1 || newName.Length < 1)
        {
            throw new FormatException("Введите ФИО и рекомендации");
        }

        var repository = new PatientsRepository();
        repository.UpdateItem(patient, newHospitalCode, newDoctorTin, newDiagnosisCode, newHospitalizationDate,
            newExtractDate, newName, newTin, newConditionDischarge);
    }
}