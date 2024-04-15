using System.ComponentModel.DataAnnotations;

namespace Storage2.Models
{
    public class Product
    {

        public int Id { get; set; }

        public string Name { get; set; }

        [Range(1, 1000000)]
        public int Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime Orderdate { get; set; }
        public string Category { get; set; }
        public string Shelf { get; set; }

        [Range(1, 10000)]
        public int Count { get; set; }
        public string Description { get; set; }

    }
}
