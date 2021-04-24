using System;
using System.Threading.Tasks;
using Filps.Application.Requests.Files.Commands.SaveFile;
using Filps.Application.Requests.Files.Queries.DownloadFile;
using Filps.Application.Requests.Files.Queries.GetFile;
using Filps.Application.Requests.GoogleDrive.Queries.DownloadFile;
using Filps.Application.Requests.GoogleDrive.Queries.GetFiles;
using Filps.Domain.Enums;
using Filps.Domain.Models.Files;
using Filps.Models.Files;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filps.Controllers
{
    [Route("api/files")]
    public class FileController : MediatorController
    {
        [HttpPost("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFiles([FromBody] GetFilesInput input)
        {
            var request = input.Storage switch
            {
                Storage.GoogleDrive => Mapper.Map(input, new GetGoogleDriveFilesQuery(Session.Id, Email)),
                Storage.OneDrive => Mapper.Map(input, new GetGoogleDriveFilesQuery(Session.Id, Email)),
                Storage.Dropbox => Mapper.Map(input, new GetGoogleDriveFilesQuery(Session.Id, Email)),
                _ => throw new NotSupportedException()
            };
            
            return Ok(await Mediator.Send(request));
        }
        
        [HttpPost("download")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DownloadFile([FromBody] GetFileInput input)
        {
            var request = (IRequest<FileData>)(input.Storage switch
            {
                Storage.GoogleDrive => Mapper.Map(input, new DownloadGoogleDriveFileQuery(Session.Id, Email)),
                Storage.OneDrive => Mapper.Map(input, new DownloadGoogleDriveFileQuery(Session.Id, Email)),
                Storage.Dropbox => Mapper.Map(input, new DownloadGoogleDriveFileQuery(Session.Id, Email)),
                Storage.Filps => Mapper.Map(input, new DownloadFileQuery(Session.Id, Email)),
                _ => throw new Exception("Invalid request")
            });
            
            var file = await Mediator.Send(request);

            if (file == null) return NoContent();
            
            return File(file.Content, "application/xml",$"{file.Name}.idf");
        }
        
        [HttpPost("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFile([FromBody] GetFileInput input)
        {
            var request = input.Storage switch
            {
                Storage.GoogleDrive => Mapper.Map(input, new GetFileQuery(input.FileId)),
                Storage.OneDrive => Mapper.Map(input, new GetFileQuery(input.FileId)),
                Storage.Dropbox => Mapper.Map(input, new GetFileQuery(input.FileId)),
                Storage.Filps => Mapper.Map(input, new GetFileQuery(input.FileId)),
                _ => throw new Exception("Invalid request")
            } as IBaseRequest;

            return Ok(await Mediator.Send(request));
        }
        
        [HttpPost("save")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveFile([FromBody] SaveFileInput input)
        {
            var request = input.Storage switch
            {
                Storage.GoogleDrive => Mapper.Map(input, new SaveFileCommand(Session.Id, Email)),
                Storage.OneDrive => Mapper.Map(input, new SaveFileCommand(Session.Id, Email)),
                Storage.Dropbox => Mapper.Map(input, new SaveFileCommand(Session.Id, Email)),
                Storage.Filps => Mapper.Map(input, new SaveFileCommand(Session.Id, Email)),
                _ => throw new Exception("Invalid request")
            } as IBaseRequest;

            var id = await Mediator.Send(request) as string;
            
            return Ok(new { id });
        }
        
        [HttpPost("open")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> OpenFile([FromForm] OpenFileInput input)
        {
            throw new NotImplementedException();
        }
    }
}