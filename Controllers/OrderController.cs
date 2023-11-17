using FinekraApi.Core.Entities;
using FinekraApi.Core.Models;
using FinekraApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

using Serilog;
using System;

[Produces("application/json")]
[Route("Orders")]
public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly Serilog.ILogger _logger;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));

        
        _logger = new LoggerConfiguration()
            .WriteTo.File("audit_log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    [HttpPost("AddToCart")]
    public IActionResult AddToCart([FromBody] OrderModificationModel model)
    {
        try
        {
            var order = _orderService.GetOrderById(model.OrderId);
            if (order == null)
            {
                return NotFound() as IActionResult;
            }

            _orderService.AddToCart(order, model.OrderDetail);

            // Audit Log: Sepete Ekleme
            LogAudit("AddToCart", model.OrderId, model.OrderDetail.PerfumeId, model.OrderDetail.OrderDetailId, model.OrderDetail.Count);

            return Ok(order);
        }
        catch (Exception ex)
        {
            // Hata durumunu günlüğe kaydet
            _logger.Error(ex, $"Hata Sepete Ekleme: {ex.Message}");
            return BadRequest(ex.Message) as IActionResult;
        }
    }

    [HttpPut("UpdateCartItem")]
    public IActionResult UpdateCartItem([FromBody] OrderModificationModel model)
    {
        try
        {
            var order = _orderService.GetOrderById(model.OrderId);
            if (order == null)
            {
                return NotFound() as IActionResult;
            }

            _orderService.UpdateCartItem(order, model.OrderDetailId, model.Count);

            // Audit Log: Sepet Ürünü Güncelleme
            LogAudit("UpdateCartItem", model.OrderId, 0, model.OrderDetailId, model.Count);

            return Ok(order);
        }
        catch (Exception ex)
        {
            // Hata durumunu günlüğe kaydet
            _logger.Error(ex, $"Hata Sepet Ürünü Güncelleme: {ex.Message}");
            return BadRequest(ex.Message) as IActionResult;
        }
    }

    [HttpDelete("RemoveFromCart")]
    public IActionResult RemoveFromCart([FromBody] OrderModificationModel model)
    {
        try
        {
            var order = _orderService.GetOrderById(model.OrderId);
            if (order == null)
            {
                return NotFound() as IActionResult;
            }

            _orderService.RemoveFromCart(order, model.OrderDetailId);

            // Audit Log: Sepetten Kaldırma
            LogAudit("RemoveFromCart", model.OrderId, 0, model.OrderDetailId, 0);

            return Ok(order);
        }
        catch (Exception ex)
        {
            // Hata durumunu günlüğe kaydet
            _logger.Error(ex, $"Hata Sepetten Kaldırma: {ex.Message}");
            return BadRequest(ex.Message) as IActionResult;
        }
    }

    private void LogAudit(string action, int orderId, int perfumeId, int orderDetailId, int quantity)
    {
        var auditLog = new AuditLogModel
        {
            UserName = HttpContext.User.Identity.Name,
            Action = action,
            OrderId = orderId,
            PerfumeId = perfumeId,
            OrderDetailId = orderDetailId,
            Timestamp = DateTime.Now,
            AddedQuantity = action == "AddToCart" ? quantity : 0,
            RemovedQuantity = action == "RemoveFromCart" ? quantity : 0,
            UpdatedQuantity = action == "UpdateCartItem" ? quantity : 0
        };

        // Serilog ile log dosyasına yaz
        _logger.Information("{@AuditLog}", auditLog);
    }

}
