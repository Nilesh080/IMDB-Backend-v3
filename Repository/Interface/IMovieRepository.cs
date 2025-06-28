using IMDBApi_Assignment3.Models.DB;

namespace IMDBApi_Assignment3.Repository.Interface
{
    public interface IMovieRepository
    {
        List<(Movie Movie, (List<int> ActorIds, List<int> GenreIds))> GetAll();
        List<(Movie Movie, (List<int> ActorIds, List<int> GenreIds))> GetAll(int year);
        (Movie Movie, (List<int> ActorIds, List<int> GenreIds)) GetById(int id);
        void Create(Movie movie, List<int> actorIds, List<int> genreIds);
        void Update(Movie movie, List<int> actorIds, List<int> genreIds);
        void Delete(int id);
        bool Exists(int id);
    }
}
