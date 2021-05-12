using System.Threading.Tasks;
using Filps.Application.Requests.Files.Commands.DeleteFile;
using Filps.Application.Requests.Files.Commands.SaveFile;
using Filps.Application.Requests.Files.Commands.ToggleFilePin;
using Filps.Application.Requests.Files.Queries.DownloadFile;
using Filps.Application.Requests.Files.Queries.GetFile;
using Filps.Application.Requests.Files.Queries.GetUserFiles;
using Filps.Application.Requests.Files.Queries.OpenFile;
using Filps.Attributes;
using Filps.Models.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filps.Controllers
{
    [JwtAuthorize]
    [Route("api/files")]
    public class FileController : MediatorController
    {
        [HttpPost("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFiles([FromBody] GetFilesInput input)
        {
            var request = Mapper.Map(input, new GetUserFilesQuery(Email));
            return Ok(await Mediator.Send(request));
        }
        
        [HttpPost("download")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DownloadFile([FromBody] GetFileInput input)
        {
            var request = Mapper.Map(input, new DownloadFileQuery(Email));
            
            var file = await Mediator.Send(request);

            if (file == null) return NoContent();
            
            return File(file.Content, "application/xml",$"{file.Name}.idf");
        }
        
        [HttpPost("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFile([FromBody] GetFileInput input)
        {
            var request = new GetFileQuery(input.FileId);
            return Ok(await Mediator.Send(request));
        }
        
        [HttpPost("save")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveFile([FromBody] SaveFileInput input)
        {
            var request = Mapper.Map(input, new SaveFileCommand(Email));
            var id = await Mediator.Send(request);
            
            return Ok(new { id });
        }
        
        [AllowAnonymous]
        [HttpPost("open")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> OpenFile([FromForm] OpenFileInput input)
        {
            var request = Mapper.Map(input, new OpenFileQuery(Email));
            return Ok(await Mediator.Send(request));
        }
        
        [HttpPost("togglePin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> TogglePin([FromBody] GetFileInput input)
        {
            return Ok(await Mediator.Send(new ToggleFilePinCommand(input.FileId, Email)));
        }
        
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteFile(string id)
        {
            return Ok(await Mediator.Send(new DeleteFileCommand(id, Email)));
        }
    }
}