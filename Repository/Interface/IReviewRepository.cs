using IMDBApi_Assignment3.Models.DB;

namespace IMDBApi_Assignment3.Repository.Interface
{
    public interface IReviewRepository
    {
        List<int> GetByMovieId(int movieId);
        Review GetById(int id);
        void Create(Review review);
        void Update(Review review);
        void Delete(int id);
        void DeleteByMovieId(int movieId);
        bool Exists(int id);
    }
}
