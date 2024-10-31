using System.Collections.Generic;

namespace ContenfulAPI.Models
{
    public class ContentTypeModel
    {
        // Constructor to initialize required properties
        public ContentTypeModel(string id, string name)
        {
            Id = id;
            Name = name;
            Fields = new List<ContentField>();
        }

        // Required properties
        public string Id { get; set; }
        public string Name { get; set; }

        // Optional properties
        public string? Description { get; set; }
        public List<ContentField> Fields { get; set; }
        public string? Type { get; set; }
    }

    // ContentField class definition for fields within ContentTypeModel
    public class ContentField
    {
        public string Id { get; set; } = string.Empty;  // Required by default
        public string Name { get; set; } = string.Empty;  // Required by default
        public string Type { get; set; } = string.Empty;  // Required by default
        public bool Required { get; set; } = false;  // Optional field

        // Optional properties
        public string? Description { get; set; }
    }
}
