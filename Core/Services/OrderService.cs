using FinekraApi.Core.Entities;
using FinekraApi.Core.Interfaces;
using Serilog;
using Serilog.Sinks.File;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FinekraApi.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly Serilog.ILogger _logger;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            
            _logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        }

        public Orders CreateOrder(int userDetailId, string shipAddress, int phone, List<OrderDetails> orderDetails)
        {
            Orders newOrder = new Orders
            {
                UserDetailId = userDetailId,
                ShipAddress = shipAddress,
                Phone = phone,
                OrderDate = DateTime.Now,
                OrderDetails = orderDetails
            };

            try
            {
                _orderRepository.AddOrder(newOrder);
                Log.Information("New order created. OrderId: {OrderId}", newOrder.OrderId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating a new order.");
                throw; 
            }

            return newOrder;
        }

        public void AddToCart(Orders order, OrderDetails orderDetail)
        {
            order.OrderDetails.Add(orderDetail);

            try
            {
                _orderRepository.UpdateOrder(order);
                Log.Information("Item added to cart. OrderId: {OrderId}, OrderDetailId: {OrderDetailId}", order.OrderId, orderDetail.OrderDetailId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while adding an item to the cart.");
                throw; 
            }
        }

        public void UpdateCartItem(Orders order, int orderDetailsId, int count)
        {
            var orderDetail = order.OrderDetails.FirstOrDefault(od => od.OrderDetailId == orderDetailsId);

            if (orderDetail != null)
            {
                orderDetail.Count = count;

                try
                {
                    _orderRepository.UpdateOrder(order);
                    Log.Information("Cart item updated. OrderId: {OrderId}, OrderDetailId: {OrderDetailId}, NewCount: {NewCount}", order.OrderId, orderDetail.OrderDetailId, count);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error while updating a cart item.");
                    throw; 
                }
            }
        }

        public void RemoveFromCart(Orders order, int orderDetailsId)
        {
            var orderDetail = order.OrderDetails.FirstOrDefault(od => od.OrderDetailId == orderDetailsId);

            if (orderDetail != null)
            {
                order.OrderDetails.Remove(orderDetail);

                try
                {
                    _orderRepository.UpdateOrder(order);
                    Log.Information("Item removed from cart. OrderId: {OrderId}, OrderDetailId: {OrderDetailId}", order.OrderId, orderDetail.OrderDetailId);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error while removing an item from the cart.");
                    throw; 
                }
            }
        }

        public Orders GetOrderById(int orderId)
        {
            return _orderRepository.GetOrderById(orderId);
        }

        public IEnumerable<Orders> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }
        private void LogInformation(string action, int orderId, int productId, int quantity)
        {
            _logger.Information("{Action} - OrderId: {OrderId}, ProductId: {ProductId}, Quantity: {Quantity}, Timestamp: {Timestamp}, User: {User}",
                action, orderId, productId, quantity, DateTime.Now, "SystemUser");
        }

        private void LogError(string action, Exception ex)
        {
            _logger.Error(ex, "{Action} - Timestamp: {Timestamp}, User: {User}", action, DateTime.Now, "SystemUser");
        }
    }
}
