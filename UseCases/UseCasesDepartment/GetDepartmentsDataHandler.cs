using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCases.UseCasesDepartment;

public class GetDepartmentsDataHandler
{
    public List<Department> Handle(List<string> hospitalCodeFilter, List<string> codeFilter, List<string> nameFilter,
        List<string> managerFilter)
    {
        var repository = new DepartmentsRepository();
        return repository.ShowTable(hospitalCodeFilter, codeFilter, nameFilter, managerFilter);
    }
}