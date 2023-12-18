using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCases.UseCasesPatients;

public class GetPatientsDataHandler
{
    public List<Patient> Handle(List<string> hospitalCodeFilter, List<string> doctorTinFilter,
        List<string> diagnosisCodeFilter, List<string> nameFilter, List<string> tinFilter,
        List<string> conditionsDischargeFilter, List<DateOnly> hospitalizationDateFilter,
        List<DateOnly> extractDateFilter)
    {
        var repository = new PatientsRepository();
        return repository.ShowTable(hospitalCodeFilter, doctorTinFilter, nameFilter, diagnosisCodeFilter,
            hospitalizationDateFilter, extractDateFilter, tinFilter, conditionsDischargeFilter);
    }
}