using Filps.Application.Models;
using MediatR;

namespace Filps.Application.Requests.Files.Commands.ToggleFilePin
{
    public class ToggleFilePinCommand : UserRequest, IRequest
    {
        public ToggleFilePinCommand(string id, string email) : base(email)
        {
            Id = id;
        }
        
        public string Id { get; set; }
    }
}