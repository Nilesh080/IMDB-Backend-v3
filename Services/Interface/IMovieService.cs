using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Models.DTOs.Response;

namespace IMDBApi_Assignment3.Services.Interface
{
    public interface IMovieService
    {
        List<MovieResponse> GetAll();
        List<MovieResponse> GetAll(int year);
        MovieResponse GetById(int id);
        (string Message, int Id) Create(MovieRequest movie);
        string Update(int id, MovieRequest movieRequest);
        void Delete(int id);
    }
}
