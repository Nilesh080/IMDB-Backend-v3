using IMDBApi_Assignment3.Models.DB;
using IMDBApi_Assignment3.Repository.Interface;

namespace IMDBApi_Assignment3.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private static List<Genre> _genreList = new List<Genre>();
        private static int _nextId = 1;
        public void Create(Genre genre)
        {
            genre.Id = _nextId++;
            _genreList.Add(genre);
        }

        public void Delete(int id)
        {
            _genreList.Remove(GetById(id));
        }

        public List<Genre> GetAll()
        {
            return _genreList;
        }

        public Genre GetById(int id)
        {
            return _genreList.First(g => g.Id == id);
        }

        public void Update(Genre genre)
        {
            var index = _genreList.IndexOf(genre);
            _genreList[index] = genre;
        }
        public bool Exists(int id)
        {
            return _genreList.Any(g => g.Id == id);
        }
    }
}
