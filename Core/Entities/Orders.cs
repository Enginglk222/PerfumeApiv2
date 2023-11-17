using System.ComponentModel.DataAnnotations;

namespace FinekraApi.Core.Entities
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public int UserDetailId { get; set; }
        public string ShipAddress { get; set; }
        public int Phone { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }

}
