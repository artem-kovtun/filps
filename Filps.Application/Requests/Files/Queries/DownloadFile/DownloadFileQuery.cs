﻿using Filps.Application.Models;
using Filps.Domain.Models.Files;
using MediatR;

namespace Filps.Application.Requests.Files.Queries.DownloadFile
{
    public class DownloadFileQuery : UserRequest, IRequest<FileData>
    {
        public DownloadFileQuery( string email) : base(email) { }

        public string FileId { get; set; }
    }
}