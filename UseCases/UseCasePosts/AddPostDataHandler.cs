using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCasePosts;

public class AddPostDataHandler
{
    public void Handle(string code, string name, string branch)
    {
        if (code.Contains(" ") || code.Length != 8)
        {
            throw new FormatException("Код должности должен состоять из 8 символов");
        }        
        if (name.Length < 1 || branch.Length < 1)
        {
            throw new FormatException("Введите название и отделение");
        }

        var repository = new PostRepository();
        repository.AddItem(code, name, branch);
    }
}