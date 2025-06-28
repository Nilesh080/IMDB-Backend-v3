using IMDBApi_Assignment3.Models.DB;
using IMDBApi_Assignment3.Repository.Interface;

namespace IMDBApi_Assignment3.Repository
{
    public class ProducerRepository : IProducerRepository
    {
        private static List<Person> _producers = new List<Person>();
        private static int _nextId = 1;
        public void Create(Person producer)
        {
            producer.Id = _nextId++;
            _producers.Add(producer);
        }

        public void Delete(int id)
        {
            _producers.Remove(GetById(id));
        }

        public List<Person> GetAll()
        {
            return _producers;
        }

        public Person GetById(int id)
        {
            return _producers.First(p => p.Id == id);
        }

        public void Update(Person producer)
        {
            int index = _producers.IndexOf(producer);
            _producers[index] = producer;
        }
        public bool Exists(int id)
        {
            return _producers.Any(p => p.Id == id);
        }
    }
}
