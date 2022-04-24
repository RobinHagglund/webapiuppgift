namespace webapiuppgift.Models.Product
{
    public class ProductUpdateRequest
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
    }
}
