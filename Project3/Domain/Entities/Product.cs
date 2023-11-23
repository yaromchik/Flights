using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flights.Domain.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public string Image { get; set; }
        public Guid ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public virtual ProductType Type { get; set; }
    }
}
