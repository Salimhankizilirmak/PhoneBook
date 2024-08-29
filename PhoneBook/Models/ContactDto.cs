using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class ContactDto
    {
        [Required,MaxLength(100)]
        public string Name { get; set; } ="";
        [Required,MaxLength(100)]
        public string Email { get; set; } = "";
        [Required,MaxLength(100)]
        public string PhoneNumber { get; set; } = "";
        
        public IFormFile? ImageFile { get; set; }
    }
}
