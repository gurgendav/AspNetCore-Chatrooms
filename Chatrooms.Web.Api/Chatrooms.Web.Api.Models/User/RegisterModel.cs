using System.ComponentModel.DataAnnotations;

namespace Chatrooms.Web.Api.Models.User
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
