using System.ComponentModel.DataAnnotations;

namespace IMDBApi_Assignment3.Models.DTOs.Request
{
    public class ReviewRequest
    {
        [Required(ErrorMessage = "Review message is required")]
        public string Message { get; set; }
    }
}
