using Microsoft.AspNetCore.Http;

namespace Filps.Models.Files
{
    public class OpenFileInput
    {
        public IFormFile File { get; set; }
    }
}