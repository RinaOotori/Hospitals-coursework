using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCases.UseCasesDoctors;

public class GetDoctorsDataHandler
{
    public List<Doctor> Handle(List<string> hospitalCodeFilter, List<string> nameFilter, List<string> departmentCodeFilter,
        List<string> tinFilter, List<string> postCodeFilter)
    {
        var repository = new DoctorsRepository();
        return repository.ShowTable(hospitalCodeFilter, nameFilter, postCodeFilter, tinFilter, departmentCodeFilter);
    }
}