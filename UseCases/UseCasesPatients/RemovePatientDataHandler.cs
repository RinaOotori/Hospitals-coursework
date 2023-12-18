using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCases.UseCasesPatients;

public class RemovePatientDataHandler
{
    public void Handle(string id)
    {
        var repository = new PatientsRepository();
        repository.RemoveItem(id);
    }
}