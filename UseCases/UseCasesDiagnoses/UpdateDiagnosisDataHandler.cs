using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCasesDiagnoses;

public class UpdateDiagnosisDataHandler
{
    public void Handle(Diagnosis diagnosis, string newCode, string newName, string newMethod)
    {
        if (newCode.Contains(" ") || newCode.Length != 3)
        {
            throw new FormatException("Код диагноза должен состоять из 3 символов");
        }
        if (newName.Length < 1 || newMethod.Length < 1)
        {
            throw new FormatException("Введите название диагноза и метод лечения");
        }

        var repository = new DiagnosesRepository();
        repository.UpdateItem(diagnosis, newCode, newName, newMethod);
    }
}