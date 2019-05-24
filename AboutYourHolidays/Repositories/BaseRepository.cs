using System;
using AboutYourHolidays.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AboutYourHolidays.Repositories
{
    public abstract class BaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public abstract DbSet<T> DataCollection { get; }

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return DataCollection;
        }

        public T Get(int id)
        {
            return DataCollection.Find(id);
        }

        public T Add(T item)
        {
            return DataCollection.Add(item);
        }

        public void Delete(int id)
        {
            Delete(Get(id));
        }

        public void Delete(T item2Del)
        {
            DataCollection.Remove(item2Del);
            _context.SaveChanges();
        }
    }
}