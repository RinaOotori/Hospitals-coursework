using Microsoft.EntityFrameworkCore;

namespace hospitalsCoursework.Repository;

public class DoctorsRepository : BaseRepository
{
    public List<Doctor> ShowTable(List<string> hospitalCodeFilter, List<string> nameFilter, List<string> postCodeFilter,
        List<string> tinFilter, List<string> departmentCodeFilter)
    {
        {
            var dbSet = this.Db.Doctors;
            var result = dbSet.ToList();
            if (hospitalCodeFilter.Count > 0 && !hospitalCodeFilter.Contains("Все"))
            {
                result = dbSet.Where(x => hospitalCodeFilter.Contains(x.HospitalCode)).ToList();
            }

            if (nameFilter.Count > 0 && !nameFilter.Contains("Все"))
            {
                result = result.Where(x => nameFilter.Contains(x.Name)).ToList();
            }

            if (postCodeFilter.Count > 0 && !postCodeFilter.Contains("Все"))
            {
                result = result.Where(x => postCodeFilter.Contains(x.PostCode)).ToList();
            }

            if (tinFilter.Count > 0 && !tinFilter.Contains("Все"))
            {
                result = result.Where(x => tinFilter.Contains(x.Tin)).ToList();
            }
            
            if (departmentCodeFilter.Count > 0 && !departmentCodeFilter.Contains("Все"))
            {
                result = result.Where(x => departmentCodeFilter.Contains(x.DepartmentCode)).ToList();
            }

            return result;
        }
    }

    public void AddItem(string hospitalCode, string postCode, string name, string departmentCode, string tin)
    {
        var newItem = new Doctor
        {
            HospitalCode = hospitalCode, Name = name, PostCode = postCode, Tin = tin, DepartmentCode = departmentCode
        };
        {
            Db.Doctors.Add(newItem);
            Db.SaveChanges();
        }
    }

    public void UpdateItem(Doctor doctor, string newTin, string newName, string newHospitalCode,
        string newDepartmentCode, string newPostCode)
    {
        {
            Db.Doctors
                .Where(x => x.Id == doctor.Id)
                .ExecuteUpdate(x => x
                    .SetProperty(d => d.HospitalCode, d => newHospitalCode)
                    .SetProperty(d => d.Name, d => newName)
                    .SetProperty(d => d.Tin, d => newTin)
                    .SetProperty(d => d.DepartmentCode, d => newDepartmentCode)
                    .SetProperty(d => d.PostCode, d => newPostCode));
            Db.SaveChanges();
        }
    }

    public void RemoveItem(string id)
    {
        Db.Doctors.Remove(GetById(id));
        Db.SaveChanges();
    }

    private Doctor? FindById(string id)
    {
        return Db.Doctors.FirstOrDefault(x => x.Id == int.Parse(id));
    }
    
    private Doctor GetById(string id)
    {
        var doctor = FindById(id);
        if (doctor == null)
        {
            throw new NullReferenceException();
        }

        return doctor;
    }
}