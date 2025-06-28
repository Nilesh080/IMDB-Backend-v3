using IMDBApi_Assignment3.Models.DTOs;
using IMDBApi_Assignment3.Models.DTOs.Request;

namespace IMDBApi_Assignment3.Validations.Interface
{
    public interface IUserValidation
    {
        void ValidateSignupRequest(SignUpRequest request);
        void ValidateLoginRequest(LoginRequest request);
    }
}
