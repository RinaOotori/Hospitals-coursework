namespace hospitalsCoursework.Repository;

public class BaseRepository
{
    protected HospitalsContext Db;

    protected BaseRepository()
    {
        Db = new HospitalsContext();
    }
}