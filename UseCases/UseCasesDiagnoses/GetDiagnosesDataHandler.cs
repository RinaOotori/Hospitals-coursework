using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCasesDiagnoses;

public class GetDiagnosesDataHandler
{
    public List<Diagnosis> Handle(List<string> codeFilter, List<string> nameFilter, List<string> methodFilter)
    {
        var repository = new DiagnosesRepository();
        return repository.ShowTable(codeFilter, nameFilter, methodFilter);
    }
}