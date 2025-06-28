using IMDBApi_Assignment3.Models.DB;
using IMDBApi_Assignment3.Repository.Interface;

namespace IMDBApi_Assignment3.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private static readonly List<Person> _actors = new List<Person>();
        private static int _nextId = 1;

        public List<Person> GetAll()
        {
            return _actors;
        }

        public Person GetById(int id)
        {
            return _actors.First(a => a.Id == id);
        }

        public void Create(Person actor)
        {
            actor.Id = _nextId++;
            _actors.Add(actor);
        }

        public void Update(Person actor)
        {
            var existingActorIndex = _actors.FindIndex(a => a.Id == actor.Id);
            _actors[existingActorIndex] = actor;
        }

        public void Delete(int id)
        {
            var actor = _actors.First(a => a.Id == id);
            _actors.Remove(actor);
        }

        public bool Exists(int id)
        {
            return _actors.Any(a => a.Id == id);
        }
    }
}