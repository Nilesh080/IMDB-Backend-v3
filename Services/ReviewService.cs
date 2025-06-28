using IMDBApi_Assignment3.Models.DB;
using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Models.DTOs.Response;
using IMDBApi_Assignment3.Repository.Interface;
using IMDBApi_Assignment3.Services.Interface;
using IMDBApi_Assignment3.Validations.Interface;

namespace IMDBApi_Assignment3.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewValidation _reviewValidation;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewValidation reviewValidation, IReviewRepository reviewRepository)
        {
            _reviewValidation = reviewValidation;
            _reviewRepository = reviewRepository;
        }
        public List<ReviewResponse> GetByMovieId(int movieId)
        {
            _reviewValidation.ValidateMovieId(movieId);

            var reviews = _reviewRepository.GetByMovieId(movieId);
            return reviews.Select(reviewId => MapToResponse(_reviewRepository.GetById(reviewId))).ToList();
        }

        public ReviewResponse GetById(int id, int movieId)
        {
            _reviewValidation.ValidateMovieId(movieId);

            _reviewValidation.ValidateId(id);

            _reviewValidation.ValidateReviewBelongsToMovie(id, movieId);

            var review = _reviewRepository.GetById(id);

            return MapToResponse(review);
        }

        public (string Message, int Id) Create(int movieId, ReviewRequest request)
        {
            _reviewValidation.ValidateMovieId(movieId);
            _reviewValidation.ValidateRequest(request);

            var review = new Review
            {
                Message = request.Message,
                MovieId = movieId
            };

            _reviewRepository.Create(review);

            return ($"Review for Movie Id: {movieId} created successfully.", review.Id);
        }

        public string Update(int movieId, int id, ReviewRequest request)
        {
            _reviewValidation.ValidateMovieId(movieId);
            _reviewValidation.ValidateId(id);
            _reviewValidation.ValidateReviewBelongsToMovie(id, movieId);
            _reviewValidation.ValidateRequest(request);

            var existingReview = _reviewRepository.GetById(id);

            existingReview.Message = request.Message;
            existingReview.MovieId = movieId;

            _reviewRepository.Update(existingReview);

            return $"Review Id: {id} updated successfully.";
        }

        public void Delete(int movieId, int id)
        {
            _reviewValidation.ValidateMovieId(movieId);
            _reviewValidation.ValidateId(id);
            _reviewValidation.ValidateReviewBelongsToMovie(id, movieId);

            _reviewRepository.Delete(id);
        }

        public void DeleteAllByMovieId(int movieId)
        {
            _reviewValidation.ValidateMovieId(movieId);

            _reviewRepository.DeleteByMovieId(movieId);
        }

        private ReviewResponse MapToResponse(Review review)
        {
            return new ReviewResponse
            {
                Id = review.Id,
                Message = review.Message
            };
        }
    }
}
