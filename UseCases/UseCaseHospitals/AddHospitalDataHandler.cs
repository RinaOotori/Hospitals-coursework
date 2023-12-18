using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCaseHospitals;

public class AddHospitalDataHandler
{
    public void Handle(string code, string name, string address, string tin, string age)
    {
        if (code.Contains(" ") || code.Length != 6)
        {
            throw new FormatException("Код больницы должен состоять из 6 символов");
        }        
        if (name.Length < 1 || address.Length < 1)
        {
            throw new FormatException("Введите название и адрес");
        }        
        if (tin.Length != 10 || tin.Contains(" "))
        {
            throw new FormatException("ИНН больницы должен состоять из 10 символов");
        }        
        if (age != "Взрослые" || age != "Дети")
        {
            throw new FormatException(@"Допустимые значения поля ""Возраст"": ""Дети"", ""Взрослые""");
        }

        var repository = new HospitalRepository();
        repository.AddItem(code, name, address, tin, age);
    }
}