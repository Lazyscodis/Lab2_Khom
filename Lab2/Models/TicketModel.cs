using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab2.Models
{
    public class TicketModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Route { get; set; }

        [Required]
        public string Class { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть положительным значением.")]
        public decimal Price { get; set; }

        public int? StationId { get; set; }

        [ForeignKey("StationId")]
        public StationModel? Station { get; set; }
    }
}