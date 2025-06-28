using System.ComponentModel.DataAnnotations;
using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Repository.Interface;
using IMDBApi_Assignment3.Validations.Interface;

namespace IMDBApi_Assignment3.Validations
{
    public class GenreValidation : IGenreValidation
    {
        private readonly IGenreRepository _genreRepository;

        public GenreValidation(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Actor ID must be greater than zero");

            if (!_genreRepository.Exists(id))
            {
                throw new KeyNotFoundException($"Genre with ID {id} not found");
            }
        }

        public void ValidateRequest(GenreRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "Genre request cannot be null");

            var existingGenre = _genreRepository.GetAll()
                                .FirstOrDefault(g => g.Name.Equals(request.Name.Trim(), StringComparison.OrdinalIgnoreCase));

            if (existingGenre != null)
            {
                throw new ArgumentException($"Genre '{request.Name}' already exists");
            }

            if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length < 2 || request.Name.Length > 50)
                throw new ValidationException("Genre name must be between 2 and 50 characters");
        }
    }
}
