namespace Filps.Domain.Models.Files
{
    public class GetFilesFilters
    {
        public string Email { get; set; }
        public int? Page { get; set; }
        public int? Take { get; set; }
        public string Search { get; set; }
    }
}