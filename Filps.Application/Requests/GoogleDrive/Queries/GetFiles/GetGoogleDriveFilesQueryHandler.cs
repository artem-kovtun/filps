using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Filps.Application.Factories.Contracts;
using Filps.Application.Models.Storage;
using Filps.GoogleServices.Contacts;
using Filps.GoogleServices.Models.Drive;
using MediatR;

namespace Filps.Application.Requests.GoogleDrive.Queries.GetFiles
{
    public class GetGoogleDriveFilesQueryHandler : IRequestHandler<GetGoogleDriveFilesQuery, GetFilesResponse>
    {
        private static readonly string[] Scopes = 
        { 
            "https://www.googleapis.com/auth/drive", 
            "https://www.googleapis.com/auth/userinfo.profile", 
            "https://www.googleapis.com/auth/userinfo.email"
        };
        
        private readonly IGoogleCredentialEngine _googleCredentialEngine;
        private readonly IGoogleDriveEngine _googleDriveEngine;
        private readonly IDataStoreFactory _dataStoreFactory;
        private readonly IMapper _mapper;
        
        public GetGoogleDriveFilesQueryHandler(
            IGoogleCredentialEngine googleCredentialEngine,
            IGoogleDriveEngine googleDriveEngine, 
            IMapper mapper, 
            IDataStoreFactory dataStoreFactory)
        {
            _googleCredentialEngine = googleCredentialEngine;
            _googleDriveEngine = googleDriveEngine;
            _mapper = mapper;
            _dataStoreFactory = dataStoreFactory;
        }
        
        public async Task<GetFilesResponse> Handle(GetGoogleDriveFilesQuery request, CancellationToken cancellationToken)
        {
            var user = $"{request.Email ?? request.SessionId}//Google";
            
            //var credential1 = await _googleAuthProvider.GetCredentialAsync(null, cancellationToken);
            var credential = await _googleCredentialEngine.GetCredentialAsync(
                Scopes,
                user,
                cancellationToken, 
                _dataStoreFactory.DataStore(request.SessionId, request.Email));

            var files = await _googleDriveEngine.GetMyDriveContentAsync(credential, new StorageFilters
            {
                ParentId = request.ParentId,
                Search = request.Search
            });

            return _mapper.Map<GetFilesResponse>(files);
        }
    }
}