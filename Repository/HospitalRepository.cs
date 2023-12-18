using Microsoft.EntityFrameworkCore;

namespace hospitalsCoursework.Repository;

public class HospitalRepository : BaseRepository
{
    public List<Hospital> ShowTable(List<string> codeFilter, List<string> nameFilter, List<string> addressFilter,
        List<string> tinFilter, List<string> ageFilter)
    {
        {
            var dbSet = this.Db.Hospitals;
            var result = dbSet.ToList();
            if (codeFilter.Count > 0 && !codeFilter.Contains("Все"))
            {
                result = dbSet.Where(x => codeFilter.Contains(x.HospitalCode)).ToList();
            }

            if (nameFilter.Count > 0 && !nameFilter.Contains("Все"))
            {
                result = result.Where(x => nameFilter.Contains(x.Name)).ToList();
            }
            
            if (addressFilter.Count > 0 && !addressFilter.Contains("Все"))
            {
                result = result.Where(x => addressFilter.Contains(x.Address)).ToList();
            }
            
            if (tinFilter.Count > 0 && !tinFilter.Contains("Все"))
            {
                result = result.Where(x => tinFilter.Contains(x.Tin)).ToList();
            }
            
            if (ageFilter.Count > 0 && !ageFilter.Contains("Все"))
            {
                result = result.Where(x => ageFilter.Contains(x.AgePatients)).ToList();
            }

            return result;
        }
    }

    public void AddItem(string code, string name, string address, string tin, string age)
    {
        var newItem = new Hospital { HospitalCode = code, Name = name, Address = address, Tin = tin, AgePatients = age};
        {
            Db.Add(newItem);
            Db.SaveChanges();
        }
    }

    public void UpdateItem(Hospital hospital, string newCode, string newName, string newAddress, string newTin, string newAge)
    {
        {
            Db.Hospitals
                .Where(x => x.Id == hospital.Id)
                .ExecuteUpdate(x => x
                    .SetProperty(h => h.HospitalCode,h => newCode)
                    .SetProperty(h => h.Name, h => newName)
                    .SetProperty(h => h.Address, h => newAddress)
                    .SetProperty(h => h.AgePatients, h => newAge)
                    .SetProperty(h => h.Tin, h => newTin));
            Db.SaveChanges();
        }
    }

    public void RemoveItem(string id)
    {
        Db.Hospitals.Remove(GetById(id));
        Db.SaveChanges();
    }

    private Hospital? FindById(string id)
    {
        return Db.Hospitals.FirstOrDefault(x => x.Id == int.Parse(id));
    }

    private Hospital GetById(string id)
    {
        var hospital = FindById(id);
        if (hospital == null)
        {
            throw new NullReferenceException();
        }

        return hospital;
    }
}