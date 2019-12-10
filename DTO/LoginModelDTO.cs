using System.ComponentModel.DataAnnotations;

namespace AperoBoxApi.DTO
{
    public class LoginModelDTO
    {
        [Required]
        public string Username {get; set; }
        [Required]
        public string Password {get; set; }
    }
}