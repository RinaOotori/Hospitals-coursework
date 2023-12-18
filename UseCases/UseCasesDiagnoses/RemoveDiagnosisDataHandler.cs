using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCasesDiagnoses;

public class RemoveDiagnosisDataHandler
{
    public void Handle(string id)
    {
        var repository = new DiagnosesRepository();
        repository.RemoveItem(id);
    }
}