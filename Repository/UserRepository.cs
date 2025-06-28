using IMDBApi_Assignment3.Models.DB;
using IMDBApi_Assignment3.Repository.Interface;

public class UserRepository : IUserRepository
{
    private static List<User> _users = new List<User>();
    private static int _nextUserId = 1;

    public void Create(User user)
    {
        user.Id = _nextUserId++;
        _users.Add(user);
    }

    public User GetByEmail(string email)
    {
        return _users.First(u => u.Email.Equals(email));
    }

    public List<User> GetAll()
    {
        return _users;
    }
    public bool Exists(string email)
    {
        return _users.Any(u => u.Email.Equals(email));
    }
}
