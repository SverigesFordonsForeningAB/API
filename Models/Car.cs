using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SverigesFordonsFörening.Data
{
    public class Car
    {

        [Key]
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal CarPrice { get; set; }
    }
}
