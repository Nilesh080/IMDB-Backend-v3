using IMDBApi_Assignment3.Models.DTOs.Request;

namespace IMDBApi_Assignment3.Validations.Interface
{
    public interface IReviewValidation
    {
        void ValidateMovieId(int movieId);
        void ValidateId(int reviewId);
        void ValidateRequest(ReviewRequest request);
        void ValidateReviewBelongsToMovie(int reviewId, int movieId);
    }
}
