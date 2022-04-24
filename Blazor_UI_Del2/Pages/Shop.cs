namespace Blazor_UI_Del2.Pages
{
    public class Shop
    {
        public class Product
        {
            public string ArticleNumber { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string CategoryName { get; set; }

            public string ImgUrl { get; set; }
            public decimal Price { get; set; }
        }

        public class CartProduct
        {
            public int Quantity { get; set; }
            public Product Product { get; set; }

            public decimal Total
            {
                get
                {
                    return Product.Price * Quantity;
                }
            }
        }

        public class Cart
        {
            public List<CartProduct> Items { get; set; } = new List<CartProduct>();

            public Decimal Total
            {
                get
                {
                    decimal total = (decimal)0.0;
                    foreach (var item in Items)
                    {
                        total += item.Total;
                    }
                    return total;
                }
            }
            public DateTime LastAccessed { get; set; }
            public int TimeToLiveInSeconds { get; set; } = 30; // default
        }
    }
}
