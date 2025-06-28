using IMDBApi_Assignment3.Models.DB;

namespace IMDBApi_Assignment3.Repository.Interface
{
    public interface IGenreRepository
    {
        List<Genre> GetAll();
        Genre GetById(int id);
        void Create(Genre genre);
        void Update(Genre genre);
        void Delete(int id);
        bool Exists(int id);
    }
}
