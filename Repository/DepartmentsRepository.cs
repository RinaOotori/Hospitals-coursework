using Microsoft.EntityFrameworkCore;

namespace hospitalsCoursework.Repository;

public class DepartmentsRepository : BaseRepository
{
    public List<Department> ShowTable(List<string> hospitalCodeFilter, List<string> codeFilter, List<string> nameFilter,
        List<string> managerFilter)
    {
        var dbSet = this.Db.Departments;
        var result = dbSet.ToList();
        if (hospitalCodeFilter.Count > 0 && !hospitalCodeFilter.Contains("Все"))
        {
            result = dbSet.Where(x => hospitalCodeFilter.Contains(x.HospitalCode)).ToList();
        }

        if (codeFilter.Count > 0 && !codeFilter.Contains("Все"))
        {
            result = result.Where(x => codeFilter.Contains(x.DepartmentCode)).ToList();
        }

        if (nameFilter.Count > 0 && !nameFilter.Contains("Все"))
        {
            result = result.Where(x => nameFilter.Contains(x.Name)).ToList();
        }

        if (managerFilter.Count > 0 && !managerFilter.Contains("Все"))
        {
            result = result.Where(x => managerFilter.Contains(x.Manager)).ToList();
        }

        return result;
    }

    public void AddItem(string hospitalCode, string code, string name, string manager)
    {
        var newItem = new Department
            { HospitalCode = hospitalCode, DepartmentCode = code, Name = name, Manager = manager };
        {
            Db.Departments.Add(newItem);
            Db.SaveChanges();
        }
    }

    public void UpdateItem(Department department, string newHospitalCode, string newCode, string newName, string newManager)
    {
        {
            Db.Departments
                .Where(x => x.Id == department.Id)
                .ExecuteUpdate(x => x
                    .SetProperty(p => p.HospitalCode, p => newHospitalCode)
                    .SetProperty(p => p.DepartmentCode, p => newCode)
                    .SetProperty(p => p.Manager, p => newManager)
                    .SetProperty(p => p.Name, p => newName));
            Db.SaveChanges();
        }
    }

    public void RemoveItem(string id)
    {
        Db.Departments.Remove(GetById(id));
        Db.SaveChanges();
    }

    private Department? FindById(string id)
    {
        return Db.Departments.FirstOrDefault(x => x.Id == int.Parse(id));
    }

    private Department GetById(string id)
    {
        var department = FindById(id);
        if (department == null)
        {
            throw new NullReferenceException();
        }

        return department;
    }

    public List<string> GetByDepartmentCode(string code)
    {
        return Db.Departments.Where(x => x.DepartmentCode == code).Select(x => x.HospitalCode).ToList();
    }
}