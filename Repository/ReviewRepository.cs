using IMDBApi_Assignment3.Models.DB;
using IMDBApi_Assignment3.Repository.Interface;

namespace IMDBApi_Assignment3.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private static List<Review> _reviews = new List<Review>();
        private static Dictionary<int, List<int>> _movieReview = new Dictionary<int, List<int>>();
        private static int _nextId = 1;

        public void Create(Review review)
        {
            review.Id = _nextId++;

            _reviews.Add(review);

            if (!_movieReview.ContainsKey(review.MovieId))
            {
                _movieReview[review.MovieId] = new List<int>();
            }

            _movieReview[review.MovieId].Add(review.Id);
        }

        public void DeleteByMovieId(int movieId)
        {
            if (_movieReview.ContainsKey(movieId))
            {
                var reviewIdsToDelete = _movieReview[movieId];

                _reviews.RemoveAll(r => reviewIdsToDelete.Contains(r.Id));

                _movieReview.Remove(movieId);
            }
        }

        public void Delete(int id)
        {
            var review = GetById(id);

            _reviews.Remove(review);

            _movieReview[review.MovieId].Remove(id);

            if (_movieReview[review.MovieId].Count == 0)
            {
                _movieReview.Remove(review.MovieId);
            }
        }

        public List<int> GetByMovieId(int movieId)
        {
            if (!_movieReview.ContainsKey(movieId))
                return new List<int>();
            return _movieReview[movieId];
        }

        public Review GetById(int id)
        {
            return _reviews.First(review => review.Id == id);
        }

        public void Update(Review review)
        {
            int index = _reviews.IndexOf(review);
            _reviews[index] = review;
        }
        public bool Exists(int id)
        {
            return _reviews.Any(r => r.Id == id);
        }
    }
}
