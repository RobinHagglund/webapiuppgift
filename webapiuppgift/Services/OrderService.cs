using AutoMapper;
using webapiuppgift.Data;
using webapiuppgift.Models.Order;
using webapiuppgift.Models.Product;
using webapiuppgift.Models.User;

namespace webapiuppgift.Services
{
    public interface IOrderService
    {
        Task<Order> CreateAsync(List<Product> shoppingcart, User user);
    }
    public class OrderService : IOrderService
    {
        private readonly DatebaseContext _db;
        private readonly IMapper _map;

        public OrderService(DatebaseContext db, IMapper map)
        {
            _db = db;
            _map = map;
        }

        public async Task<Order> CreateAsync(List<Product> shoppingcart, User user)
        {
            var orderEntity = new OrderEntity
            {
                CustomerName = $"{user.FirstName} {user.LastName}",
                Address = $"{user.Adress} {user.ZipCode} {user.City}",
            };

            var orderRows = new List<OrderRowEntity>();
            foreach (var item in shoppingcart)
                orderRows.Add(new OrderRowEntity
                {
                    OrderId = orderEntity.Id,
                    ArticleNumber = item.ArticleNumber,
                    ProductName = item.Name,
                    Quantity = orderRows.Count,
                    ProductPrice = item.Price,
                });
            orderEntity.OrderRows = orderRows;

            _db.Orders.Add(orderEntity);
            await _db.SaveChangesAsync();

            return null!;
        }
            
    }
}
