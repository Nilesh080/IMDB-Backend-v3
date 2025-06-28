using IMDBApi_Assignment3.Models.DB;
using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Models.DTOs.Response;
using IMDBApi_Assignment3.Repository.Interface;
using IMDBApi_Assignment3.Services.Interface;
using IMDBApi_Assignment3.Validations.Interface;

namespace IMDBApi_Assignment3.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IGenreValidation _genreValidation;

        public GenreService(IGenreRepository genreRepository, IGenreValidation genreValidation)
        {
            _genreRepository = genreRepository;
            _genreValidation = genreValidation;
        }

        public List<GenreResponse> GetAll()
        {
            var genres = _genreRepository.GetAll();
            return genres.Select(MapToResponse).ToList();
        }

        public GenreResponse GetById(int id)
        {
            _genreValidation.ValidateId(id);

            var genre = _genreRepository.GetById(id);

            return MapToResponse(genre);
        }

        public (string Message, int Id) Create(GenreRequest request)
        {
            _genreValidation.ValidateRequest(request);

            var genre = new Genre
            {
                Name = request.Name
            };

            _genreRepository.Create(genre);

            return ($"Genre '{genre.Name}' created successfully.", genre.Id);
        }

        public string Update(int id, GenreRequest request)
        {
            _genreValidation.ValidateId(id);

            _genreValidation.ValidateRequest(request);

            var existingGenre = _genreRepository.GetById(id);

            existingGenre.Name = request.Name;

            _genreRepository.Update(existingGenre);

            return $"Genre id: '{id}' updated successfully.";
        }

        public void Delete(int id)
        {
            _genreValidation.ValidateId(id);
            _genreRepository.Delete(id);
        }

        private GenreResponse MapToResponse(Genre genre)
        {
            return new GenreResponse
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }
    }
}