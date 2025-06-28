using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using IMDBApi_Assignment3.Models.DTOs;
using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Repository.Interface;
using IMDBApi_Assignment3.Validations.Interface;

namespace IMDBApi_Assignment3.Validations
{
    public class UserValidation : IUserValidation
    {
        private readonly IUserRepository _userRepository;

        public UserValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void ValidateSignupRequest(SignUpRequest request)
        {
            if (_userRepository.Exists(request.Email))
                throw new ArgumentException("User already exists with provided email.");

            if (string.IsNullOrWhiteSpace(request.FirstName) ||
                request.FirstName.Length < 2 || request.FirstName.Length > 50)
            {
                throw new ValidationException("First name must be between 2 and 50 characters.");
            }

            if (string.IsNullOrWhiteSpace(request.LastName) ||
                request.LastName.Length < 2 || request.LastName.Length > 50)
            {
                throw new ValidationException("Last name must be between 2 and 50 characters.");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ValidationException("Email is required.");

            if (!IsValidEmail(request.Email))
                throw new ValidationException("A valid email address is required.");

            ValidatePassword(request.Password);

            if (request.Password != request.ConfirmPassword)
                throw new ValidationException("Password and Confirm Password do not match.");
        }

        public void ValidateLoginRequest(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ValidationException("Email is required.");

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new ValidationException("Password is required.");

            if (!IsValidEmail(request.Email))
                throw new ValidationException("A valid email address is required.");

            if (!_userRepository.Exists(request.Email))
                throw new KeyNotFoundException("User does not exist.");

            var user = _userRepository.GetByEmail(request.Email);

            if (user.Password != request.Password)
                throw new ArgumentException("Invalid email or password.");
        }

        private void ValidatePassword(string password)
        {
            if (!Regex.IsMatch(password, @"[A-Z]"))
                throw new ValidationException("Password must contain at least one uppercase letter.");

            if (!Regex.IsMatch(password, @"[a-z]"))
                throw new ValidationException("Password must contain at least one lowercase letter.");

            if (!Regex.IsMatch(password, @"[0-9]"))
                throw new ValidationException("Password must contain at least one digit.");

            if (!Regex.IsMatch(password, @"[^a-zA-Z0-9]"))
                throw new ValidationException("Password must contain at least one special character.");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
