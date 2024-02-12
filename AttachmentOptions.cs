namespace AspNetBeginner
{
    public class AttachmentOptions
    {
        public string AllowedExtension { get; set; } 
        public int MaxSizeInMegabytes { get; set; }
        public bool EnableCompression { get; set; }
    }
}
