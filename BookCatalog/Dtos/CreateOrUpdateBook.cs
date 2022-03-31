using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Dtos
{
    public class CreateOrUpdateBook
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [Range(1, 100)]
        public decimal Price { get; set; }
    }
}
