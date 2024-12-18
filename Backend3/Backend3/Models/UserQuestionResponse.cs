using System.ComponentModel.DataAnnotations;

namespace Backend3.Models
{
    public class UserQuestionResponse
    {
        [Required]
        public string? Response { get; set; } = null;

    }
}
