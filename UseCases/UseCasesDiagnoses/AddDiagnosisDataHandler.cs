using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCasesDiagnoses;

public class AddDiagnosisDataHandler
{
    public void Handle(string code, string name, string method)
    {
        if (code.Contains(" ") || code.Length != 3)
        {
            throw new FormatException("Код диагноза должен состоять из 3 символов");
        }
        if (name.Length < 1 || method.Length < 1)
        {
            throw new FormatException("Введите название диагноза и метод лечения");
        }

        var repository = new DiagnosesRepository();
        repository.AddItem(code, name, method);
    }
}