using System.ComponentModel.DataAnnotations;

namespace IMDBApi_Assignment3.Models.DTOs.Request
{
    public class PersonRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateOnly DOB { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        public string Bio { get; set; }
    }
}
