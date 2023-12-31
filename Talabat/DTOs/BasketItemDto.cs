using System.ComponentModel.DataAnnotations;

namespace Talabat.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be one item at least!!")]
        public int Quantity { get; set; }
        [Required]
        [Range(0.1, int.MaxValue,ErrorMessage ="Price must be Greater than Zero!!")]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; }
    }
}