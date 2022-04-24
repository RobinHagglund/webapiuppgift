namespace webapiuppgift.Models.Order
{
    public class HandleOrder
    {
        public int ProductId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
    }

    public class UpdateOrder
    {
        public string CustomerName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; } = null!;
        public List<UpdateOrderRow> OrderRows { get; set; } = new();
    }

    public class UpdateOrderRow
    {
        public int ProductNumber { get; set; }
        public string ProductName { get; set; } = null!;
        public string ArticleNumber { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }

    }
}
