namespace MovieZone.Service.DTOs.AWS.Storage
{
    using System.IO;

    public class GetFileByKeyDTO
    {
        public Stream FileStream { get; set; }

        public string FileType { get; set; }
    }
}
