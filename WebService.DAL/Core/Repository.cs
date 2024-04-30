
using Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebService.DAL.Core;

namespace DAL.Core
{
    public class Repository<T> : IRepositary<T> where T : class, IEntity
    {

         private readonly IApplicationDbContext _db;

        public Repository(IApplicationDbContext db)
        {
            _db = db;
            if (db != null) {throw new NullReferenceException(nameof(db));}

        }

        public void Create(T item)
        {
            _db.Entry(item).State = EntityState.Added;
            (_db as DbContext).Database.EnsureCreated();

        }

        public void Update(T item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public async Task<T> FindAsync(Guid id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public void Delete(T item)
        {
            _db.Entry(item).State = EntityState.Deleted;
        }

    }

}