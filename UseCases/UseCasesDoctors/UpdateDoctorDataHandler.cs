using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCases.UseCasesDoctors;

public class UpdateDoctorDataHandler
{
    public void Handle(Doctor doctor, string newHospitalCode, string newName, string newDepartmentCode, string newTin,
        string newPostCode)
    {
        if (newName.Length < 1)
        {
            throw new FormatException("Введите ФИО врача");
        }        
        if (newTin.Length != 10 || newTin.Contains(" "))
        {
            throw new FormatException("ИНН должен состоять из 10 символов");
        }
        var repository = new DoctorsRepository();

        repository.UpdateItem(doctor, newTin, newName, newHospitalCode, newDepartmentCode, newPostCode);
    }
}