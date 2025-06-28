using IMDBApi_Assignment3.Models.DTOs;
using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Models.DTOs.Response;


namespace IMDBApi_Assignment3.Services.Interface
{
    public interface IUserService
    {
        string Signup(SignUpRequest request);
        LoginResponse Login(LoginRequest request);
        List<UserResponse> GetAll();
    }
}