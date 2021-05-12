using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Filps.Application.Models.Storage;
using Filps.GoogleServices.Contacts;
using Filps.GoogleServices.Models.Drive;
using Google.Apis.Auth.OAuth2;
using MediatR;

namespace Filps.Application.Requests.GoogleDrive.Queries.GetFiles
{
    public class GetGoogleDriveFilesQueryHandler : IRequestHandler<GetGoogleDriveFilesQuery, GetFilesResponse>
    {
        private readonly IGoogleDriveEngine _googleDriveEngine;
        private readonly IMapper _mapper;
        
        public GetGoogleDriveFilesQueryHandler(IGoogleDriveEngine googleDriveEngine, IMapper mapper)
        {
            _googleDriveEngine = googleDriveEngine;
            _mapper = mapper;
        }
        
        public async Task<GetFilesResponse> Handle(GetGoogleDriveFilesQuery request, CancellationToken cancellationToken)
        {
            var credential = GoogleCredential.FromAccessToken("ya29.a0AfH6SMDgEpkdSA14td6BKEjPv9tCVk0wypR9iRoMjkjj56JxVZOh0ZTIk3spC4I4u8ALiYTAQ3sx_LxbSjcHWjwGGZFOf67NbCmWDJfaDgCbdjoyrlEJTma3ez9Cn82XAisiDCfNiLf3911spXcezoUK84fm");
            
            var files = await _googleDriveEngine.GetMyDriveContentAsync(credential, new StorageFilters
            {
                ParentId = request.ParentId,
                Search = request.Search
            });

            return _mapper.Map<GetFilesResponse>(files);
        }
    }
}