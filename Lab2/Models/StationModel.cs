using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab2.Models
{
    public class StationModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Schedule { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Количество платформ должно быть больше нуля.")]
        public int Platforms { get; set; }

        [Required]
        [Range(1800, 2100, ErrorMessage = "Год открытия должен быть между 1800 и 2100.")]
        public int OpeningYear { get; set; }

        public ICollection<TicketModel>? Tickets { get; set; }
    }
}