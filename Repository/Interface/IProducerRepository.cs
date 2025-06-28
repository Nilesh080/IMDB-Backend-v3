using IMDBApi_Assignment3.Models.DB;

namespace IMDBApi_Assignment3.Repository.Interface
{
    public interface IProducerRepository
    {
        List<Person> GetAll();
        Person GetById(int id);
        void Create(Person producer);
        void Update(Person producer);
        void Delete(int id);
        bool Exists(int id);
    }
}
