using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCase;

public class RemovePostDataHandler
{
    public void Handle(string id)
    {
        var repository = new PostRepository();
        repository.RemoveItem(id);
    }
}