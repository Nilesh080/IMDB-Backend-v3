using System.ComponentModel.DataAnnotations;
using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Repository.Interface;
using IMDBApi_Assignment3.Validations.Interface;

namespace IMDBApi_Assignment3.Validations
{
    public class ReviewValidation : IReviewValidation
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewValidation(IMovieRepository movieRepository, IReviewRepository reviewRepository)
        {
            _movieRepository = movieRepository;
            _reviewRepository = reviewRepository;
        }
        public void ValidateMovieId(int movieId)
        {
            if (movieId <= 0)
                throw new ArgumentException("Movie ID must be greater than zero");

            if (!_movieRepository.Exists(movieId))
            {
                throw new KeyNotFoundException($"Movie Id {movieId} does not exists.");
            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Review ID must be greater than zero");

            if (!_reviewRepository.Exists(id))
            {
                throw new KeyNotFoundException($"Review with ID {id} not found");
            }
        }

        public void ValidateRequest(ReviewRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "Review request cannot be null");

            if (string.IsNullOrWhiteSpace(request.Message) || request.Message.Length < 5 || request.Message.Length > 1000)
                throw new ValidationException("Review message must be between 5 and 1000 characters");
        }

        public void ValidateReviewBelongsToMovie(int id, int movieId)
        {
            var reviewIdsForMovie = _reviewRepository.GetByMovieId(movieId);

            if (!reviewIdsForMovie.Contains(id))
            {
                throw new InvalidOperationException($"Review with ID {id} does not belong to Movie with ID {movieId}");
            }
        }
    }
}
