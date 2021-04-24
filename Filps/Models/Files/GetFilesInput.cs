namespace Filps.Models.Files
{
    public class GetFilesInput
    {
        public Domain.Enums.Storage Storage { get; set; }
        public string ParentId { get; set; }
        public string Search { get; set; }
    }
}