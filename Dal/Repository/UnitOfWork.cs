using Dal.Data;
using Dal.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

         public ICategoryRepository Category { get; private set; }
        //  public ICoverTypeRepository CoverType { get; private set; }
        public IProductRepository Product { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
              Category = new CategoryRepository(_db);
            //   CoverType = new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);
        }


        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected virtual async ValueTask DisposeAsync(bool disposing)
        {

            if (!_disposed)
            {
                await _db.DisposeAsync();
            }
            _disposed = true;
        }
    }
}
