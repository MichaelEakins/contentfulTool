namespace ContenfulAPI.Models
{
    public class BlogPostModel
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public List<string>? Tags { get; set; }
        public string? EntryId { get; set; }
    }
}
