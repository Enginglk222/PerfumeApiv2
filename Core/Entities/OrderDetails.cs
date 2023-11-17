using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinekraApi.Core.Entities
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("Perfume")]
        public int PerfumeId { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }

        public Perfumes Perfume { get; set; }
    }
}
