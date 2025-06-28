using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Models.DTOs.Response;

namespace IMDBApi_Assignment3.Services.Interface
{
    public interface IProducerService
    {
        List<PersonResponse> GetAll();
        PersonResponse GetById(int id);
        (string Message, int Id) Create(PersonRequest producerRequest);
        string Update(int id, PersonRequest producerRequest);
        void Delete(int id);
    }
}
