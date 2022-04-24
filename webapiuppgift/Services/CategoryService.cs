using AutoMapper;
using Microsoft.EntityFrameworkCore;
using webapiuppgift.Data;
using webapiuppgift.Models.Product;

namespace webapiuppgift.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAllCategories();
    }
    public class CategoryService : ICategoryService
    {
        private readonly DatebaseContext _db;
        private readonly IMapper _map;
        public CategoryService(DatebaseContext db, IMapper map)
        {
            _db = db;
            _map = map;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllCategories()
        {
            return _map.Map<IEnumerable<ProductCategory>>(await _db.ProductCategories.ToListAsync());
        }
    }
}
