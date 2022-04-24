using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapiuppgift.Data;
using webapiuppgift.Forms;
using webapiuppgift.Models.Order;

namespace webapiuppgift.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly DatebaseContext _context;

        public OrdersController(DatebaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder(OrderForm model)
        {
            var user = await _context.Users.FindAsync(model.UserId);

            if (user == null) return NotFound();

            if (model.Cart.Count == 0) return BadRequest();

            var order = new OrderEntity
            {
                CustomerName = user.FirstName + " " + user.LastName,
                Address = user.Adress + ", " + user.ZipCode + ", " + user.City,
                OrderDate = model.OrderDate,
                OrderStatus = model.OrderStatus,
            };

            var orderRows = new List<OrderRowEntity>();

            foreach (var item in model.Cart)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null) return BadRequest();
                orderRows.Add(new OrderRowEntity
                {
                    ProductName = product.Name,
                    ArticleNumber = product.ArticleNr,
                    ProductPrice = product.Price,
                    Quantity = item.Quantity,
                    OrderId = order.Id,
                });
            }

            order.OrderRows = orderRows;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return new OkObjectResult(order);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllOrders() => new OkObjectResult(await _context.Orders.Include(x => x.OrderRows).ToListAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrderById(int id)
        {
            var order = await _context.Orders.Include(x => x.OrderRows).FirstOrDefaultAsync(x => x.Id == id);

            if (order == null) return NotFound();

            return new OkObjectResult(order);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrderById(int id, UpdateOrder updateModel)
        {
            var order = await _context.Orders.Include(x => x.OrderRows).FirstOrDefaultAsync(x => x.Id == id);

            if (order == null) return NotFound();

            order.OrderDate = updateModel.OrderDate;
            order.Address = updateModel.Address;
            order.OrderStatus = updateModel.OrderStatus;
            order.CustomerName = updateModel.CustomerName;

            var orderRows = new List<OrderRowEntity>(order.OrderRows);
            foreach (var item in updateModel.OrderRows)
            {
                var orderRow = orderRows.FirstOrDefault(x => x.ArticleNumber == item.ArticleNumber);
                if (orderRow == null)
                {
                    orderRows.Add(new OrderRowEntity
                    {
                        OrderId = order.Id,
                        ArticleNumber = item.ArticleNumber,
                        ProductName = item.ProductName,
                        ProductPrice = item.ProductPrice,
                        Quantity = item.Quantity,
                    });
                }
                else
                {
                    orderRow.Quantity = item.Quantity;
                    orderRow.ArticleNumber = item.ArticleNumber;
                    orderRow.ProductName = item.ProductName;
                    orderRow.ProductPrice = item.ProductPrice;
                    _context.Entry(orderRow).State = EntityState.Modified;
                }
            }

            order.OrderRows = orderRows;
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new OkObjectResult(order);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderById(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
