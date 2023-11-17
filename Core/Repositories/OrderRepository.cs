using FinekraApi.Core.Entities;
using FinekraApi.Core.Interfaces;
using System.Linq;

namespace FinekraApi.Core.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PerfumeDbContext _dbContext; // Gerçek veritabanı bağlantısı

        public OrderRepository(PerfumeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Orders AddOrder(Orders order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return order;
        }

        public void UpdateOrder(Orders order)
        {
            var existingOrder = _dbContext.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);

            if (existingOrder != null)
            {
                // Güncelleme işlemleri burada gerçekleştirilir.
                existingOrder.UserDetailId = order.UserDetailId;
                existingOrder.ShipAddress = order.ShipAddress;
                existingOrder.Phone = order.Phone;
                existingOrder.OrderDate = order.OrderDate;

                _dbContext.SaveChanges();
            }
        }

        public Orders GetOrderById(int orderId)
        {
            return _dbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public IEnumerable<Orders> GetAllOrders()
        {
            return _dbContext.Orders.ToList();
        }

        public void RemoveOrder(Orders order)
        {
            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();
        }
    }
}
