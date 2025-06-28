using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Models.DTOs.Response;

namespace IMDBApi_Assignment3.Services.Interface
{
    public interface IReviewService
    {
        List<ReviewResponse> GetByMovieId(int movieId);
        ReviewResponse GetById(int id, int movieId);
        (string Message, int Id) Create(int movieId, ReviewRequest reviewRequest);
        string Update(int movieId, int id, ReviewRequest reviewRequest);
        void Delete(int movieId, int id);
        void DeleteAllByMovieId(int movieId);
    }
}
