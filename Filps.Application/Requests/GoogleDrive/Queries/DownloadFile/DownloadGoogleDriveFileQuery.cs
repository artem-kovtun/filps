using Filps.Application.Models;
using Filps.Domain.Models.Files;
using MediatR;

namespace Filps.Application.Requests.GoogleDrive.Queries.DownloadFile
{
    public class DownloadGoogleDriveFileQuery : UserRequest, IRequest<FileData>
    {
        public DownloadGoogleDriveFileQuery(string email) : base(email) { }

        public string FileId { get; set; }
    }
}