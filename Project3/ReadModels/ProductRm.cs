using Flights.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flights.ReadModels
{
    public class ProductRm
    {
        public Guid Id;
        public string Name;
        public string Description;
        public int Cost;
        public string Image;
        public ProductType Type;
    }
}
