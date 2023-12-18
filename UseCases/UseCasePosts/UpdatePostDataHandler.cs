using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCase;

public class UpdatePostDataHandler
{
    public void Handle(Post post, string newCode, string newName, string newBranch)
    {
        if (newCode.Contains(" ") || newCode.Length != 8)
        {
            throw new FormatException("Код должности должен состоять из 8 символов");
        }        
        if (newName.Length < 1 || newBranch.Length < 1)
        {
            throw new FormatException("Введите название и отделение");
        }

        var repository = new PostRepository();
        repository.UpdateItem(post, newCode, newName, newBranch);
    }
}