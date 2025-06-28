using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Models.DTOs.Response;

namespace IMDBApi_Assignment3.Services.Interface
{
    public interface IActorService
    {
        List<PersonResponse> GetAll();
        PersonResponse GetById(int id);
        (string Message, int Id) Create(PersonRequest actorRequest);
        string Update(int id, PersonRequest actorRequest);
        void Delete(int id);
    }
}
