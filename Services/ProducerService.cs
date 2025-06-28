using IMDBApi_Assignment3.Models.DB;
using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Models.DTOs.Response;
using IMDBApi_Assignment3.Models.Enums;
using IMDBApi_Assignment3.Repository.Interface;
using IMDBApi_Assignment3.Services.Interface;
using IMDBApi_Assignment3.Validations.Interface;

namespace IMDBApi_Assignment3.Services
{
    public class ProducerService : IProducerService
    {
        private readonly IProducerValidation _producerValidation;
        private readonly IProducerRepository _producerRepository;

        public ProducerService(IProducerValidation producerValidation, IProducerRepository producerRepository)
        {
            _producerValidation = producerValidation;
            _producerRepository = producerRepository;
        }

        public List<PersonResponse> GetAll()
        {
            var producers = _producerRepository.GetAll();
            return producers.Select(MapToResponse).ToList();
        }

        public PersonResponse GetById(int id)
        {
            _producerValidation.ValidateId(id);

            var producer = _producerRepository.GetById(id);

            return MapToResponse(producer);
        }

        public (string Message, int Id) Create(PersonRequest request)
        {
            _producerValidation.ValidateRequest(request);

            Enum.TryParse<Gender>(request.Gender, true, out var genderEnum);

            var producer = new Person
            {
                Name = request.Name,
                Bio = request.Bio,
                DOB = request.DOB,
                Gender = genderEnum
            };

            _producerRepository.Create(producer);

            return ($"Producer '{producer.Name}' created successfully.", producer.Id);
        }

        public string Update(int id, PersonRequest request)
        {
            _producerValidation.ValidateId(id);
            _producerValidation.ValidateRequest(request);

            var existingproducer = _producerRepository.GetById(id);

            Enum.TryParse<Gender>(request.Gender, true, out var genderEnum);

            existingproducer.Name = request.Name;
            existingproducer.Bio = request.Bio;
            existingproducer.DOB = request.DOB;
            existingproducer.Gender = genderEnum;

            _producerRepository.Update(existingproducer);

            return $"Producer id: '{id}' updated successfully.";
        }

        public void Delete(int id)
        {
            _producerValidation.ValidateId(id);

            _producerRepository.Delete(id);
        }

        private PersonResponse MapToResponse(Person producer)
        {
            return new PersonResponse
            {
                Id = producer.Id,
                Name = producer.Name,
                Bio = producer.Bio,
                DOB = producer.DOB,
                Gender = producer.Gender
            };
        }
    }
}