using Microsoft.EntityFrameworkCore;

namespace hospitalsCoursework.Repository;

public class DiagnosesRepository : BaseRepository
{
    public List<Diagnosis> ShowTable(List<string> codeFilter, List<string> nameFilter, List<string> methodFilter)
    {
        var dbSet = this.Db.Diagnoses;
        var result = dbSet.ToList();
        if (codeFilter.Count > 0 && !codeFilter.Contains("Все"))
        {
            result = dbSet.Where(x => codeFilter.Contains(x.DiagnosisCode)).ToList();
        }

        if (nameFilter.Count > 0 && !nameFilter.Contains("Все"))
        {
            result = result.Where(x => nameFilter.Contains(x.Name)).ToList();
        }

        if (methodFilter.Count > 0 && !methodFilter.Contains("Все"))
        {
            result = result.Where(x => methodFilter.Contains(x.TreatmentMethod)).ToList();
        }

        return result;
    }

    public void AddItem(string code, string name, string method)
    {
        var newItem = new Diagnosis { DiagnosisCode = code, Name = name, TreatmentMethod = method };
        {
            Db.Diagnoses.Add(newItem);
            Db.SaveChanges();
        }
    }

    public void UpdateItem(Diagnosis diagnosis, string newCode, string newName, string newMethod)
    {
        {
            Db.Diagnoses
                .Where(x => x.Id == diagnosis.Id)
                .ExecuteUpdate(x => x
                    .SetProperty(d => d.DiagnosisCode, d => newCode)
                    .SetProperty(d => d.Name, d => newName)
                    .SetProperty(d => d.TreatmentMethod, d => newMethod));
            Db.SaveChanges();
        }
    }

    public void RemoveItem(string id)
    {
        Db.Diagnoses.Remove(GetByCode(id));
        Db.SaveChanges();
    }

    private Diagnosis? FindByCode(string id)
    {
        return Db.Diagnoses.FirstOrDefault(x => x.Id == int.Parse(id));
    }

    private Diagnosis GetByCode(string id)
    {
        var diagnosis = FindByCode(id);
        if (diagnosis == null)
        {
            throw new NullReferenceException();
        }

        return diagnosis;
    }
}