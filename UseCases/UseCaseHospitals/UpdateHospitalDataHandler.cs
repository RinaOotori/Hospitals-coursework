using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCaseHospitals;

public class UpdateHospitalDataHandler
{
    public void Handle(Hospital hospital, string newCode, string newName, string newAddress, string newTin, string newAge)
    {
        if (newCode.Contains(" ") || newCode.Length != 6)
        {
            throw new FormatException("Код больницы должен состоять из 6 символов");
        }        
        if (newName.Length < 1 || newAddress.Length < 1)
        {
            throw new FormatException("Введите название и адрес");
        }        
        if (newTin.Length != 10 || newTin.Contains(" "))
        {
            throw new FormatException("ИНН больницы должен состоять из 10 символов");
        }        
        if (newAge != "Взрослые" || newAge != "Дети")
        {
            throw new FormatException(@"Допустимые значения поля ""Возраст"": ""Дети"", ""Взрослые""");
        }


        var repository = new HospitalRepository();
        repository.UpdateItem(hospital, newCode, newName, newAddress, newTin, newAge);
    }
}