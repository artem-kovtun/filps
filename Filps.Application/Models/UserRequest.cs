namespace Filps.Application.Models
{
    public class UserRequest
    {
        public UserRequest(string email)
        {
            Email = email;
        }
        
        public string Email { get; set; }
    }
}