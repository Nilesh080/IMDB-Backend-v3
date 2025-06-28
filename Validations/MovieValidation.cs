using System.ComponentModel.DataAnnotations;
using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Repository.Interface;
using IMDBApi_Assignment3.Validations.Interface;

namespace IMDBApi_Assignment3.Validations
{
    public class MovieValidation : IMovieValidation
    {
        public readonly IMovieRepository _movieRepository;
        public readonly IActorRepository _actorRepository;
        public readonly IProducerRepository _producerRepository;
        public readonly IGenreRepository _genreRepository;

        public MovieValidation(IMovieRepository movieRepository, IActorRepository actorRepository, IProducerRepository producerRepository, IGenreRepository genreRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
            _producerRepository = producerRepository;
            _genreRepository = genreRepository;
        }
        public void ValidateActorIds(List<int> actorIds)
        {
            if (actorIds.Any(id => !_actorRepository.Exists(id)))
            {
                var invalidId = actorIds.First(id => !_actorRepository.Exists(id));
                throw new KeyNotFoundException($"Actor with ID {invalidId} was not found.");
            }
        }

        public void ValidateGenreIds(List<int> genreIds)
        {
            if (genreIds.Any(id => !_genreRepository.Exists(id)))
            {
                var invalidId = genreIds.First(id => !_genreRepository.Exists(id));
                throw new KeyNotFoundException($"Genre with ID {invalidId} was not found.");
            }
        }
        public void ValidateProducerId(int producerId)
        {
            if (!_producerRepository.Exists(producerId))
            {
                throw new KeyNotFoundException($"Producer with ID {producerId} not found");
            }
        }

        public void ValidateId(int id)
        {
            if (!_movieRepository.Exists(id))
            {
                throw new KeyNotFoundException($"Movie with ID {id} not found");
            }
        }

        public void ValidateRequest(MovieRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "Movie request cannot be null");

            if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length > 200 || request.Name.Length < 2)
                throw new ValidationException("Movie name must be between 2 and 200 characters.");

            if (request.YearOfRelease < 1900 || request.YearOfRelease > 2100)
                throw new ValidationException("Year of release must be between 1900 and 2100.");

            if (!string.IsNullOrWhiteSpace(request.Plot) && request.Plot.Length > 200)
                throw new ValidationException("Plot cannot exceed 200 characters.");

            if (request.ProducerId < 1)
                throw new ValidationException("ProducerId must be a positive number.");

            if (!string.IsNullOrWhiteSpace(request.CoverImage))
            {
                if (request.CoverImage.Length > 500)
                    throw new ValidationException("Cover image URL cannot exceed 500 characters.");

                if (!Uri.IsWellFormedUriString(request.CoverImage, UriKind.Absolute))
                    throw new ValidationException("Cover image must be a valid URL.");
            }

            if (request.ActorIds == null || request.ActorIds.Count < 1)
                throw new ValidationException("At least one actor must be selected.");

            if (request.GenreIds == null || request.GenreIds.Count < 1)
                throw new ValidationException("At least one genre must be selected.");
        }
    }
}
