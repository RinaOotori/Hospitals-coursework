using hospitalsCoursework.Repository;
using hospitalsCoursework.UseCaseHospitals;

namespace hospitalsCoursework.UseCases.UseCasesDepartment;

public class AddDepartmentDataHandler
{
    public void Handle(string hospitalCode, string code, string name, string manager)
    {
        if (code.Contains(" ") || code.Length != 6)
        {
            throw new FormatException("Код подразделения должен состоять из 6 символов");
        }        
        
        if (name.Length < 1 || manager.Length < 1)
        {
            throw new FormatException("Введите название и ФИО заведуещего");
        }
        var repository = new DepartmentsRepository();
        repository.AddItem(hospitalCode, code, name, manager);
    }
}