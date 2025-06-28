using IMDBApi_Assignment3.Models.DB;
using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Models.DTOs.Response;
using IMDBApi_Assignment3.Models.Enums;
using IMDBApi_Assignment3.Repository.Interface;
using IMDBApi_Assignment3.Services.Interface;
using IMDBApi_Assignment3.Validations.Interface;

namespace IMDBApi_Assignment3.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IActorValidation _actorValidation;

        public ActorService(IActorRepository actorRepository, IActorValidation actorValidation)
        {
            _actorRepository = actorRepository;
            _actorValidation = actorValidation;
        }

        public List<PersonResponse> GetAll()
        {
            var actors = _actorRepository.GetAll();

            return actors.Select(MapToResponse).ToList();
        }

        public PersonResponse GetById(int id)
        {
            _actorValidation.ValidateId(id);

            var actor = _actorRepository.GetById(id);

            return MapToResponse(actor);
        }

        public (string Message, int Id) Create(PersonRequest request)
        {
            _actorValidation.ValidateRequest(request);

            Enum.TryParse<Gender>(request.Gender, true, out var genderEnum);

            var actor = new Person
            {
                Name = request.Name,
                Bio = request.Bio,
                DOB = request.DOB,
                Gender = genderEnum
            };

            _actorRepository.Create(actor);

            return ($"Actor '{actor.Name}' created successfully.", actor.Id);
        }

        public string Update(int id, PersonRequest request)
        {
            _actorValidation.ValidateId(id);
            _actorValidation.ValidateRequest(request);

            var existingActor = _actorRepository.GetById(id);

            Enum.TryParse<Gender>(request.Gender, true, out var genderEnum);

            existingActor.Name = request.Name;
            existingActor.Bio = request.Bio;
            existingActor.DOB = request.DOB;
            existingActor.Gender = genderEnum;

            _actorRepository.Update(existingActor);

            return $"Actor id: '{existingActor.Id}' updated successfully.";
        }

        public void Delete(int id)
        {
            _actorValidation.ValidateId(id);

            _actorRepository.Delete(id);
        }

        private PersonResponse MapToResponse(Person actor)
        {
            return new PersonResponse
            {
                Id = actor.Id,
                Name = actor.Name,
                Bio = actor.Bio,
                DOB = actor.DOB,
                Gender = actor.Gender
            };
        }
    }
}