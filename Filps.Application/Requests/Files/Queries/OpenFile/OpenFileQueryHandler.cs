using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Filps.Domain.Models.Files;
using MediatR;
using Newtonsoft.Json;

namespace Filps.Application.Requests.Files.Queries.OpenFile
{
    public class OpenFileQueryHandler : IRequestHandler<OpenFileQuery, File>
    {
        public async Task<File> Handle(OpenFileQuery request, CancellationToken cancellationToken)
        {
            var xmlSerializer = new XmlSerializer(typeof(object));
            var content = xmlSerializer.Deserialize(request.File.Content);

            var serializedContent = JsonConvert.SerializeObject(content);
            
            return new File
            {
                Name = request.File.Name,
                Size = request.File.Size,
                SerializedContent = serializedContent
            };
        }
    }
}