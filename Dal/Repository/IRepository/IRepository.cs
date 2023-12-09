using Dal.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperty = null);
        Task<IEnumerable<T>> GetAll(string? includeProperty = null);
         IEnumerable<T>  GetAllInclude(string? includeProperty = null);
        Task Add(T entity);
        Task Remove(T entity);

    }
}
