using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace TradeMarket.Models.Dto
{
    public class RequestedProduct
    {
        public static Instant currentTime = Instant.FromDateTimeUtc(System.DateTime.UtcNow);

        /*[DatabaseGenerated(DatabaseGeneratedOption.Identity)]*/
        [Key]
        public Guid RequestedProductId { get; set; }

        [Required]
        public required RequestedProduct AskingFor { get; set; }

        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }

        [Required]
        public Condition Condition { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public required User Seller { get; set; }
        public Instant LastUpdated { get; set; }
        public Instant CreatedAt { get; set; } = currentTime;
    }
}
