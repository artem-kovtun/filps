using Filps.Application.Models;
using Filps.Domain.Models.Files;
using MediatR;

namespace Filps.Application.Requests.Files.Queries.OpenFile
{
    public class OpenFileQuery: UserRequest, IRequest<File>, IRequest<Unit>
    {
        public OpenFileQuery(string sessionId, string email) : base(sessionId, email) { }
        
        public FileData File { get; set; }
    }
}