using System.ComponentModel.DataAnnotations;

namespace AperoBoxApi.Models
{
    public class LoginModel
    {
        public string Username {get; set; }
        public string Password {get; set; }
    }
}