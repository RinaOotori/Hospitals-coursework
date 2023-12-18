using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCases.UseCasesPatients;

public class AddPatientDataHandler
{
    public void Handle(string hospitalCode, string doctorTin, string diagnosisCode, DateOnly hospitalizationDate,
        DateOnly extractDate, string name, string tin, string conditionDischarge)
    {
        if (tin.Contains(" ") || tin.Length != 10)
        {
            throw new FormatException("ИНН должен состоять из 10 символов");
        }
        
        if (conditionDischarge.Length < 1 || name.Length < 1)
        {
            throw new FormatException("Введите ФИО и рекомендации");
        }

        var repository = new PatientsRepository();
        repository.AddItem(hospitalCode, doctorTin, name, diagnosisCode, hospitalizationDate, extractDate, tin,
            conditionDischarge);
    }
}