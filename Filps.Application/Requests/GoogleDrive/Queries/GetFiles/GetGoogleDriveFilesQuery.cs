using Filps.Application.Models;
using Filps.Application.Models.Storage;
using Filps.Domain.Enums;
using MediatR;

namespace Filps.Application.Requests.GoogleDrive.Queries.GetFiles
{
    public class GetGoogleDriveFilesQuery: UserRequest, IRequest<GetFilesResponse>
    {
        public GetGoogleDriveFilesQuery(string email) : base( email) { }
        public string ParentId { get; set; }
        public string Search { get; set; }
    }
}