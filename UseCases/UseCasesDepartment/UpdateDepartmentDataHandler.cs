using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCases.UseCasesDepartment;

public class UpdateDepartmentDataHandler
{
    public void Handle(Department department, string hospitalCode, string code, string name, string manager)
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
        repository.UpdateItem(department, hospitalCode, code, name, manager);
    }
}