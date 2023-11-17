using FinekraApi.Core.Entities;

namespace FinekraApi.Core.Models
{
    public class OrderCreationModel
    {
        public int UserDetailId { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
