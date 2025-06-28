using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Models.DTOs.Response;

namespace IMDBApi_Assignment3.Services.Interface
{
    public interface IGenreService
    {
        List<GenreResponse> GetAll();
        GenreResponse GetById(int id);
        (string Message, int Id) Create(GenreRequest genreRequest);
        string Update(int id, GenreRequest genreRequest);
        void Delete(int id);
    }
}
