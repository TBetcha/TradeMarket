using System.ComponentModel;
using NodaTime;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeMarket.Models.Dto
{
    public class Product
    {
        public static Instant currentTime = Instant.FromDateTimeUtc(System.DateTime.UtcNow);

        [Key]
        /*[DatabaseGenerated(DatabaseGeneratedOption.Identity)]*/
        public Guid ProductId { get; set; }

        [ForeignKey("RequestedProductId")]
        public RequestedProduct? AskingFor { get; set; }

        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }

        [Required]
        public required Status Status { get; set; }

        [Required]
        public required string Type { get; set; }

        [Required]
        public required Condition Condition { get; set; }

        [Required]
        public required Category Category { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public required User Seller { get; set; }
        public Instant LastUpdated { get; set; }
        public Instant CreatedAt { get; set; } = currentTime;
    }
}
