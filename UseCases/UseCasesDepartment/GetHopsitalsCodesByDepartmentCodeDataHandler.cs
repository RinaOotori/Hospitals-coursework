using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCases.UseCasesDepartment;

public class GetHopsitalsCodesByDepartmentCodeDataHandler
{
    public List<string> Handle(string hospitalCode)
    {
        var repository = new DepartmentsRepository();
        return repository.GetByDepartmentCode(hospitalCode);
    }
}