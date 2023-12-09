using Dal.Data;
using Dal.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            // _db.Products.Update(obj);
            var objFromDb = _db.Product.FirstOrDefault(p => p.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
                objFromDb.Price = obj.Price;
                objFromDb.Active = obj.Active;
                objFromDb.CreatedDate = obj.CreatedDate;
                objFromDb.CategoryId = obj.CategoryId;
            }
        }
    }
}
