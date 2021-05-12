using Filps.Application.Models;
using Filps.Domain.Models.Files;
using MediatR;

namespace Filps.Application.Requests.Files.Queries.OpenFile
{
    public class OpenFileQuery : UserRequest, IRequest<File>
    {
        public OpenFileQuery(string email) : base(email) { }
        
        public FileData File { get; set; }


    }
}