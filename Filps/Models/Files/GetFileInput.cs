namespace Filps.Models.Files
{
    public class GetFileInput
    {
        public Domain.Enums.Storage? Storage { get; set; }
        public string FileId { get; set; }
    }
}