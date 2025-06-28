using IMDBApi_Assignment3.Models.DB;

namespace IMDBApi_Assignment3.Repository.Interface
{
    public interface IActorRepository
    {
        List<Person> GetAll();
        Person GetById(int id);
        void Create(Person actor);
        void Update(Person actor);
        void Delete(int id);
        bool Exists(int id);
    }
}
