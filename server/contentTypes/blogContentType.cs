using ContenfulAPI.Models;
using System.Collections.Generic;

namespace ContenfulAPI.ContentTypes
{
    public class BlogContentType : ContentTypeModel
    {
        public BlogContentType() : base("blog", "Blog")
        {
            // Setting up fields for the Blog content type
            Fields = new List<ContentField>
            {
                new ContentField
                {
                    Id = "title",
                    Name = "Title",
                    Type = "Text",
                    Required = true,
                    Description = "The title of the blog post"
                },
                new ContentField
                {
                    Id = "body",
                    Name = "Body",
                    Type = "RichText",
                    Required = true,
                    Description = "The main content of the blog post"
                },
                new ContentField
                {
                    Id = "author",
                    Name = "Author",
                    Type = "Text",
                    Required = true,
                    Description = "The author of the blog post"
                },
                new ContentField
                {
                    Id = "publicationDate",
                    Name = "Publication Date",
                    Type = "Date",
                    Required = true,
                    Description = "The publication date of the blog post"
                },
                new ContentField
                {
                    Id = "tags",
                    Name = "Tags",
                    Type = "Array",
                    Required = false,
                    Description = "Tags associated with the blog post"
                }
            };
        }
    }
}
