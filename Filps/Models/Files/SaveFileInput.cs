using Filps.Domain.Enums;

namespace Filps.Models.Files
{
    public class SaveFileInput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SerializedContent { get; set; }
        public Storage Storage { get; set; }
        public string Path { get; set; }
    }
}