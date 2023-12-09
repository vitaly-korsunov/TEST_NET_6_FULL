using Dal.Data;
using Dal.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
            // dbSet.Add(entity); 
            // return true;
        }

        public async Task<IEnumerable<T>> GetAll(string? includeProperty = null)
        {
            IQueryable<T> query = dbSet;
            if (includeProperty != null)
            {
                foreach (var includeProp in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }

            }
            return await query.ToListAsync();
        }

        public IEnumerable<T> GetAllInclude(string? includeProperty = null)
        {
            IQueryable<T> query = dbSet;
            if (includeProperty != null)
            {
                foreach (var includeProp in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }

            }
            return   query.ToList();
        }

        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperty = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (includeProperty != null)
            {
                foreach (var includeProp in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }

            }
            return await query.FirstOrDefaultAsync();
        }

        public Task Remove(T entity)
        {
            dbSet.Remove(entity);
            return Task.CompletedTask;
        }


    }
}
