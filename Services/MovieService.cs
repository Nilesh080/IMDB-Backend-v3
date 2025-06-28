using IMDBApi_Assignment3.Models.DB;
using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Models.DTOs.Response;
using IMDBApi_Assignment3.Repository.Interface;
using IMDBApi_Assignment3.Services.Interface;
using IMDBApi_Assignment3.Validations.Interface;

namespace IMDBApi_Assignment3.Services
{
    public class MovieService : IMovieService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMovieValidation _movieValidation;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;

        public MovieService(IActorRepository actorRepository, IProducerRepository producerRepository, IGenreRepository genreRepository, IMovieValidation movieValidation, IReviewRepository reviewRepository, IMovieRepository movieRepository)
        {
            _actorRepository = actorRepository;
            _producerRepository = producerRepository;
            _genreRepository = genreRepository;
            _movieValidation = movieValidation;
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
        }

        public (string Message, int Id) Create(MovieRequest request)
        {
            List<int> actorIds = request.ActorIds;
            List<int> genreIds = request.GenreIds;

            _movieValidation.ValidateActorIds(actorIds);
            _movieValidation.ValidateGenreIds(genreIds);
            _movieValidation.ValidateProducerId(request.ProducerId);
            _movieValidation.ValidateRequest(request);

            Movie movieModel = new Movie
            {
                Name = request.Name,
                YearOfRelease = request.YearOfRelease,
                Plot = request.Plot,
                ProducerId = request.ProducerId,
                CoverImage = request.CoverImage
            };
            _movieRepository.Create(movieModel, actorIds, genreIds);

            return ($"Movie '{movieModel.Name}' created successfully.", movieModel.Id);
        }

        public string Update(int id, MovieRequest request)
        {
            _movieValidation.ValidateId(id);
            _movieValidation.ValidateActorIds(request.ActorIds);
            _movieValidation.ValidateGenreIds(request.GenreIds);
            _movieValidation.ValidateProducerId(request.ProducerId);
            _movieValidation.ValidateRequest(request);

            var movieModel = new Movie
            {
                Id = id,
                Name = request.Name,
                YearOfRelease = request.YearOfRelease,
                Plot = request.Plot,
                ProducerId = request.ProducerId,
                CoverImage = request.CoverImage
            };

            _movieRepository.Update(movieModel, request.ActorIds, request.GenreIds);

            return $"Movie id: '{id}' updated successfully.";
        }

        public void Delete(int id)
        {
            _movieValidation.ValidateId(id);

            _reviewRepository.DeleteByMovieId(id);
            _movieRepository.Delete(id);
        }

        public List<MovieResponse> GetAll()
        {
            var moviesResponse = _movieRepository.GetAll();

            return moviesResponse.Select(response => MapToResponse(response.Movie, response.Item2.ActorIds, response.Item2.GenreIds)).ToList();
        }

        public MovieResponse GetById(int id)
        {
            _movieValidation.ValidateId(id);

            var movie = _movieRepository.GetById(id);

            return MapToResponse(movie.Movie, movie.Item2.ActorIds, movie.Item2.GenreIds);
        }

        public List<MovieResponse> GetAll(int year)
        {
            var movies = _movieRepository.GetAll(year);

            var movieResponses = movies
                .Select(movie => MapToResponse(movie.Movie, movie.Item2.ActorIds, movie.Item2.GenreIds))
                .ToList();

            return movieResponses;
        }

        private MovieResponse MapToResponse(Movie movie, List<int> actors, List<int> genres)
        {
            return new MovieResponse
            {
                Id = movie.Id,
                Name = movie.Name,
                YearOfRelease = movie.YearOfRelease,
                Plot = movie.Plot,
                Producer = MapToPersonResponse(_producerRepository.GetById(movie.ProducerId)),
                CoverImage = movie.CoverImage,
                Actors = actors.Select(actorId => _actorRepository.GetById(actorId)).ToList().Select(actorModel => MapToPersonResponse(actorModel)).ToList(),
                Genres = genres.Select(genreId => _genreRepository.GetById(genreId)).ToList().Select(genreModel => MapToGenreResponse(genreModel)).ToList(),
            };
        }

        private PersonResponse MapToPersonResponse(Person person)
        {
            return new PersonResponse
            {
                Id = person.Id,
                Name = person.Name,
                Bio = person.Bio,
                DOB = person.DOB,
                Gender = person.Gender
            };
        }

        private GenreResponse MapToGenreResponse(Genre genre)
        {
            return new GenreResponse
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }
    }
}