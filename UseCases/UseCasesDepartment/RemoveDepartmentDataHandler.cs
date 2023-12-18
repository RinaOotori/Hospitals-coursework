using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCases.UseCasesDepartment;

public class RemoveDepartmentDataHandler
{
    public void Handle(string id)
    {
        var repository = new DepartmentsRepository();
        repository.RemoveItem(id);
    }
}