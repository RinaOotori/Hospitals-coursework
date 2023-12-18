using hospitalsCoursework.Repository;

namespace hospitalsCoursework.UseCase;

public class GetPostsDataHandler
{
    public List<Post> Handle(List<string> codeFilter, List<string> branchFilter)
    {
        var repository = new PostRepository();
        return repository.ShowTable(codeFilter, branchFilter);
    }
}