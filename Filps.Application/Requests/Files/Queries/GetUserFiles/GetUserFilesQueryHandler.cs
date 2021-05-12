using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Filps.Domain.Models.Files;
using Filps.Domain.Models.Shared;
using Filps.Domain.Repositories.Contracts;
using MediatR;

namespace Filps.Application.Requests.Files.Queries.GetUserFiles
{
    public class GetUserFilesQueryHandler : IRequestHandler<GetUserFilesQuery, PagedList<FileMetadata>>
    {
        private readonly IFileRepository _repository;
        private readonly IMapper _mapper;

        public GetUserFilesQueryHandler(IFileRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<PagedList<FileMetadata>> Handle(GetUserFilesQuery request, CancellationToken cancellationToken)
        {
            var filters = _mapper.Map<GetFilesFilters>(request);
            
            return _repository.GetUserFilesAsync(filters);
        }
    }
}