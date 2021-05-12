using Filps.Application.Models;
using MediatR;

namespace Filps.Application.Requests.Files.Commands.DeleteFile
{
    public class DeleteFileCommand : UserRequest, IRequest
    {
        public DeleteFileCommand(string id, string email) : base(email)
        {
            Id = id;
        }
        
        public string Id { get; set; }
    }
}