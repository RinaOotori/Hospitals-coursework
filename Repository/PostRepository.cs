using Microsoft.EntityFrameworkCore;

namespace hospitalsCoursework.Repository;

public class PostRepository: BaseRepository
{
    public List<Post> ShowTable(List<string> codeFilter, List<string> branchFilter)
    {
        {
            var dbSet = this.Db.Posts;
            var result = dbSet.ToList();
            if (branchFilter.Count > 0 && !branchFilter.Contains("Все"))
            {
                result = dbSet.Where(x => branchFilter.Contains(x.Branch)).ToList();
            }
            
            if (codeFilter.Count > 0 && !codeFilter.Contains("Все"))
            {
                result = dbSet.Where(x => codeFilter.Contains(x.PostCode)).ToList();
            }

            return result;
        }
    }

    public void AddItem(string code, string name, string branch)
    {
        var newItem = new Post { PostCode = code, Name = name, Branch = branch };
        {
            Db.Posts.Add(newItem);
            Db.SaveChanges();
        }
    }
    
    public void UpdateItem(Post post, string newCode, string newName, string newBranch)
    {
        {
            Db.Posts
                .Where(x => x.Id == post.Id)
                .ExecuteUpdate(x => x
                    .SetProperty(p => p.PostCode, p => newCode)
                    .SetProperty(p => p.Branch, p => newBranch)
                    .SetProperty(p => p.Name, p => newName));
            Db.SaveChanges();
        }
    }
    public void RemoveItem(string id)
    {
            Db.Posts.Remove(GetById(id));
            Db.SaveChanges();
    }
    private Post? FindById(string id)
    {
        return Db.Posts.FirstOrDefault(x => x.Id == int.Parse(id));
    }

    private Post GetById(string id)
    {
        var post = FindById(id);
        if (post == null)
        {
            throw new NullReferenceException();
        }

        return post;
    }
}