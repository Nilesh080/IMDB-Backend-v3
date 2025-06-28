using IMDBApi_Assignment3.Models.DB;
using IMDBApi_Assignment3.Repository.Interface;

namespace IMDBApi_Assignment3.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private static List<Movie> _movies = new List<Movie>();
        private static Dictionary<int, List<int>> _movieActors = new Dictionary<int, List<int>>();
        private static Dictionary<int, List<int>> _movieGenres = new Dictionary<int, List<int>>();
        private static int _nextId = 1;

        public void Create(Movie movie, List<int> actorIds, List<int> genreIds)
        {
            movie.Id = _nextId++;
            _movies.Add(movie);
            _movieActors[movie.Id] = actorIds;
            _movieGenres[movie.Id] = genreIds;
        }

        public void Delete(int id)
        {
            _movies.Remove(_movies.First(m => m.Id == id));
            _movieActors.Remove(id);
            _movieGenres.Remove(id);
        }

        public List<(Movie Movie, (List<int> ActorIds, List<int> GenreIds))> GetAll()
        {
            return _movies.Select(movie => (movie, (_movieActors[movie.Id], _movieGenres[movie.Id]))).ToList();
        }

        public List<(Movie Movie, (List<int> ActorIds, List<int> GenreIds))> GetAll(int year)
        {
            var movieList = _movies.Where(m => m.YearOfRelease == year).ToList();

            var responseList = movieList.Select(movie => (movie, (_movieActors[movie.Id], _movieGenres[movie.Id]))).ToList();

            return responseList;
        }

        public (Movie Movie, (List<int> ActorIds, List<int> GenreIds)) GetById(int id)
        {
            var movie = _movies.First(m => m.Id == id);

            return (movie, (_movieActors[id], _movieGenres[id]));
        }

        public void Update(Movie movie, List<int> actorIds, List<int> genreIds)
        {
            int index = _movies.FindIndex(m => m.Id == movie.Id);
            _movies[index] = movie;
            _movieActors[movie.Id] = actorIds;
            _movieGenres[movie.Id] = genreIds;
        }

        public bool Exists(int id)
        {
            return _movies.Any(m => m.Id == id);
        }
    }
}
