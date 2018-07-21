using System.ComponentModel.DataAnnotations;

namespace Chatrooms.Web.Api.Models.User
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}