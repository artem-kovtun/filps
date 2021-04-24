using Filps.Application.Models;
using Filps.Application.Models.Storage;
using Filps.Domain.Enums;
using MediatR;

namespace Filps.Application.Requests.GoogleDrive.Queries.GetFiles
{
    public class GetGoogleDriveFilesQuery: UserRequest, IRequest<GetFilesResponse>
    {
        public GetGoogleDriveFilesQuery(string sessionId, string email) : base(sessionId, email) { }
        
        public Storage Storage { get; set; }
        public string ParentId { get; set; }
        public string Search { get; set; }
    }
}