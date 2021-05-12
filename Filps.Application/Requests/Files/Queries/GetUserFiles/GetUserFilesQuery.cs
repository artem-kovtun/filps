using Filps.Application.Models;
using Filps.Domain.Models.Files;
using Filps.Domain.Models.Shared;
using MediatR;

namespace Filps.Application.Requests.Files.Queries.GetUserFiles
{
    public class GetUserFilesQuery : UserRequest, IRequest<PagedList<FileMetadata>>
    {
        public GetUserFilesQuery(string email) : base(email) { }
        
        public int? Page { get; set; }
        public int? Take { get; set; }
        public string Search { get; set; }
    }
}