using IMDBApi_Assignment3.Models.DTOs.Request;

namespace IMDBApi_Assignment3.Validations.Interface
{
    public interface IProducerValidation
    {
        void ValidateId(int id);
        void ValidateRequest(PersonRequest request);
    }
}
