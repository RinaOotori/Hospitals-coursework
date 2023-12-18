using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCaseHospitals;

public class GetHospitalsDataHandler
{
    public List<Hospital> Handle(List<string> codeFilter, List<string> nameFilter, List<string> addressFilter,
        List<string> tinFilter, List<string> ageFilter)
    {
        var repository = new HospitalRepository();
        return repository.ShowTable(codeFilter, nameFilter, addressFilter, tinFilter, ageFilter);
    }
}