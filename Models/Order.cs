using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SverigesFordonsFörening.Data
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("Customer")]
        public int FkCustomerId { get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey("Cars")]
        public int? FkCarId { get; set; }
        public Car? Cars { get; set; }

        [ForeignKey("Motorcycles")]
        public int? FkMotorcycleId { get; set; }
        public Motorcycle? Motorcycles { get; set; }
    }
}
