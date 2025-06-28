using IMDBApi_Assignment3.Models.DB;

namespace IMDBApi_Assignment3.Repository.Interface
{
    public interface IUserRepository
    {
        void Create(User user);
        User GetByEmail(string email);
        List<User> GetAll();
        bool Exists(string email);
    }
}
