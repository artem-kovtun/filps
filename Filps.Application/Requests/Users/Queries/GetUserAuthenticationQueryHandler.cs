using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Filps.Application.Models.User;
using Filps.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Filps.Application.Requests.Users.Queries
{
    public class GetUserAuthenticationQueryHandler : IRequestHandler<GetUserAuthenticationQuery, UserAuthentication>
    {
        private readonly ISession _session;

        public GetUserAuthenticationQueryHandler(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor?.HttpContext?.Session;
        }

        public async Task<UserAuthentication> Handle(GetUserAuthenticationQuery request, CancellationToken cancellationToken)
        {
            if (request.Email == null)
            {
                return new UserAuthentication
                {
                    Storages = new List<StorageAuthentication>
                    {
                        new StorageAuthentication
                        {
                            Storage = Storage.GoogleDrive,
                            IsAuthorized = _session.Keys.Contains($"{_session.Id}//Google")
                        },
                        new StorageAuthentication
                        {
                            Storage = Storage.OneDrive,
                            IsAuthorized = _session.Keys.Contains($"{_session.Id}//OneDrive")
                        },
                        new StorageAuthentication
                        {
                            Storage = Storage.Dropbox,
                            IsAuthorized = _session.Keys.Contains($"{_session.Id}//Dropbox")
                        }
                    }
                };
            }
            
            return new UserAuthentication();
        }
    }
}