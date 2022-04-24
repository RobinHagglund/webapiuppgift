namespace webapiuppgift.Models.Product
{
    public class Product
    {
        public string ArticleNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
    }
}
