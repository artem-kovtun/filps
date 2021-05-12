using System.Threading;
using System.Threading.Tasks;
using Filps.Domain.Repositories.Contracts;
using MediatR;

namespace Filps.Application.Requests.Files.Commands.ToggleFilePin
{
    public class ToggleFilePinCommandHandler : IRequestHandler<ToggleFilePinCommand>
    {
        private readonly IFileRepository _repository;

        public ToggleFilePinCommandHandler(IFileRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(ToggleFilePinCommand request, CancellationToken cancellationToken)
        {
            await _repository.ToggleFilePinAsync(request.Id);
            
            return Unit.Value;
        }
    }
}