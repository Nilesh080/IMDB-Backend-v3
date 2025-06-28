using IMDBApi_Assignment3.Models.DTOs.Request;

namespace IMDBApi_Assignment3.Validations.Interface
{
    public interface IMovieValidation
    {
        void ValidateActorIds(List<int> actorIds);
        void ValidateGenreIds(List<int> genreIds);
        void ValidateProducerId(int producerId);
        void ValidateId(int movieId);
        void ValidateRequest(MovieRequest request);
    }
}
