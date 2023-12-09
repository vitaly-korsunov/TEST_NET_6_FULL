using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repository.IRepository
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public ICategoryRepository Category { get; }
         
        public IProductRepository Product { get; }

        //  public void Save();
        Task Save();
    }
 
}
