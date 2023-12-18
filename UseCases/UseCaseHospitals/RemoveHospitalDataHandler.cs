using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCaseHospitals;

public class RemoveHospitalDataHandler
{
    public void Handle(string code)
    {
        var repository = new HospitalRepository();
        repository.RemoveItem(code);
    }
}