namespace Filps.Models.Files
{
    public class GetFilesInput
    {
        public int? Page { get; set; }
        public int? Take { get; set; }
        public string Search { get; set; }
    }
}