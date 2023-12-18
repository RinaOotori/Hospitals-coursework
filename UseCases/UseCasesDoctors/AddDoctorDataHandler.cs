using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCases.UseCasesDoctors;

public class AddDoctorDataHandler
{
    public void Handle(string hospitalCode, string name, string departmentCode, string tin, string postCode)
    {
        if (name.Length < 1)
        {
            throw new FormatException("Введите ФИО врача");
        }        
        if (tin.Length != 10 || tin.Contains(" "))
        {
            throw new FormatException("ИНН должен состоять из 10 символов");
        }
        var repository = new DoctorsRepository();

        repository.AddItem(hospitalCode, postCode, name, departmentCode, tin);
    }
}