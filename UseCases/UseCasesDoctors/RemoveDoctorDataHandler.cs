using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCases.UseCasesDoctors;

public class RemoveDoctorDataHandler
{
    public void Handle(string code)
    {
        var repository = new DoctorsRepository();
        repository.RemoveItem(code);
    }
}