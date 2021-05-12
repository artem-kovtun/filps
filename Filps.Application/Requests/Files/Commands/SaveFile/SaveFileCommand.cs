using Filps.Application.Models;
using MediatR;

namespace Filps.Application.Requests.Files.Commands.SaveFile
{
    public class SaveFileCommand : UserRequest, IRequest<string>
    {
        public SaveFileCommand(string email) : base( email) { }
        
        public string Id { get; set; }
        public string Name { get; set; }
        public string SerializedContent { get; set; }
    }
}