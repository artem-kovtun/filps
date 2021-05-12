using System.Threading;
using System.Threading.Tasks;
using Filps.Domain.Repositories.Contracts;
using MediatR;

namespace Filps.Application.Requests.Files.Commands.DeleteFile
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand>
    {
        private readonly IFileRepository _repository;

        public DeleteFileCommandHandler(IFileRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteFile(request.Id);
            
            return Unit.Value;
        }
    }
}