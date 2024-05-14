using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SverigesFordonsFörening.Data
{
    public class Motorcycle
    {
        [Key]
        public int MotorcycleId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal MotorcyclePrice { get; set; }
    }
}
