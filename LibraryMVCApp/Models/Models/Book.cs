using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Models.Models
{
    public class Book
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Description { get; set; }
    }
}
