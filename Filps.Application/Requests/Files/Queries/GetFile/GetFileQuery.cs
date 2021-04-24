using Filps.Domain.Models.Files;
using MediatR;

namespace Filps.Application.Requests.Files.Queries.GetFile
{
    public class GetFileQuery : IRequest<File>
    {
        public GetFileQuery(string id)
        {
            Id = id;
        }
        
        public string Id { get; set; }
    }
}