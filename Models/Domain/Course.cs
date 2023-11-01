using System.Diagnostics;

namespace FullStack.API.Models.Domain
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }    
        public string Description { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }    
        public string UrlHandle { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
         
    }
}
