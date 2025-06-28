using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IMDBApi_Assignment3.Models.DB;
using IMDBApi_Assignment3.Models.DTOs;
using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Models.DTOs.Response;
using IMDBApi_Assignment3.Repository.Interface;
using IMDBApi_Assignment3.Services.Interface;
using IMDBApi_Assignment3.Validations.Interface;
using Microsoft.IdentityModel.Tokens;

public class UserService : IUserService
{
    private readonly IUserValidation _userValidation;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserService(IUserValidation userValidation, IUserRepository userRepository, IConfiguration configuration)
    {
        _userValidation = userValidation;
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public string Signup(SignUpRequest request)
    {
        _userValidation.ValidateSignupRequest(request);

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password
        };

        _userRepository.Create(user);

        return "Account created successfully";
    }

    public LoginResponse Login(LoginRequest request)
    {
        _userValidation.ValidateLoginRequest(request);

        var user = _userRepository.GetByEmail(request.Email);

        var token = GenerateJwtToken(user);

        return new LoginResponse
        {
            Message = "Login successful",
            Token = token
        };
    }

    public List<UserResponse> GetAll()
    {
        var users = _userRepository.GetAll();

        return users.Select(u => new UserResponse
        {
            Id = u.Id,
            FullName = $"{u.FirstName} {u.LastName}",
            Email = u.Email
        }).ToList();
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = _configuration["Jwt:ValidIssuer"],
            Audience = _configuration["Jwt:ValidAudience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
