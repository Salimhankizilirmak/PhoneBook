using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = "";
        [MaxLength(100)]
        public string Email { get; set; } = "";
        [MaxLength(100)]
        public string PhoneNumber { get; set; } = "";
        [MaxLength(100)]
        public string ImageFileName { get; set; } = "";
        public DateTime CreatedAt {  get; set; }
        
    }
}
