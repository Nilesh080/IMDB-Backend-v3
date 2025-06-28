using IMDBApi_Assignment3.Models.DTOs.Request;

namespace IMDBApi_Assignment3.Validations.Interface
{
    public interface IActorValidation
    {
        public void ValidateId(int id);
        public void ValidateRequest(PersonRequest request);
    }
}
