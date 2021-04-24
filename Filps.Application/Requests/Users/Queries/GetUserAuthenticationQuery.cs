using Filps.Application.Models;
using Filps.Application.Models.User;
using MediatR;

namespace Filps.Application.Requests.Users.Queries
{
    public class GetUserAuthenticationQuery : UserRequest, IRequest<UserAuthentication>
    {
        public GetUserAuthenticationQuery(string sessionId, string email) : base(sessionId, email) { }
    }
}