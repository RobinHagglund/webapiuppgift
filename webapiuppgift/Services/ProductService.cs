using AutoMapper;
using Microsoft.EntityFrameworkCore;
using webapiuppgift.Data;
using webapiuppgift.Models.Product;

namespace webapiuppgift.Services
{
    public interface IProductService
    {
        Task<Product> CreateAsync(Product product);
        Task<IEnumerable<Product>> GetAll();
        Task<bool> DeleteAsync(string articleNumber);
        Task<Product> UpdateProductAsync (string articleNumber, ProductUpdateRequest request);
    }
    public class ProductService : IProductService
    {
        private readonly DatebaseContext _db;
        private readonly IMapper _map;
        public ProductService(DatebaseContext db, IMapper map)
        {
            _db = db;
            _map = map;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            if (!await _db.Products.AnyAsync(x => x.ArticleNr == product.ArticleNumber))
            {
                var productEntity = _map.Map<ProductEntity>(product);

                var productCatergoryEntity = await _db.ProductCategories.FirstOrDefaultAsync(x => x.Name == product.CategoryName);
                if (productCatergoryEntity == null)
                {
                    productEntity.Category = new ProductCategoryEntity { Name = product.CategoryName };
                }
                else
                {
                    productEntity.CategoryId = productCatergoryEntity.Id;
                }

                await _db.Products.AddAsync(productEntity);
                await _db.SaveChangesAsync();

                return _map.Map<Product>(productEntity);
            }
            return null!;
        }

        public async Task<bool> DeleteAsync(string articleNumber)
        {
            var productEntity = await _db.Products.FindAsync(articleNumber);
            if (productEntity != null)
            {
                _db.Products.Remove(productEntity);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return _map.Map<IEnumerable<Product>>(await _db.Products.Include(x => x.Category).ToListAsync());  
        }

        public async Task<Product> UpdateProductAsync(string articleNumber, ProductUpdateRequest request)
        {
            var productEntity = await _db.Products.FindAsync(articleNumber);
            if (productEntity != null)
            {
                if (productEntity.Name != request.Name && !string.IsNullOrEmpty(request.Name))
                    productEntity.Name = request.Name;

                if(productEntity.Description != request.Description && !string.IsNullOrEmpty(request.Description))
                    productEntity.Description = request.Description;

                if(productEntity.Price != request.Price)
                    productEntity.Price = request.Price;

                if (productEntity.ImgUrl != request.ImgUrl && !string.IsNullOrEmpty(request.ImgUrl))
                    productEntity.ImgUrl = request.ImgUrl;

                        
                _db.Entry(productEntity).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return _map.Map<Product>(productEntity);

            }
            return null!;
        }
    }
}
