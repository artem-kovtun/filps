namespace Filps.Application.Models
{
    public class UserRequest
    {
        public UserRequest(string sessionId, string email)
        {
            SessionId = sessionId;
            Email = email;
        }
        
        public string SessionId { get; set; }
        public string Email { get; set; }
    }
}