using FinekraApi.Core.Entities;

namespace FinekraApi.Core.Models
{
    public class OrderModificationModel
    {
        public int OrderId { get; set; }
        public int OrderDetailId { get; set; }
        public int Count { get; set; }
        public OrderDetails OrderDetail { get; set; }
    }
}
