using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kuzzi.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Kuzzi.DataAccess.Repository.IRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            // Giả sử T = Category
            // Lúc này dbSet = _db.Categories
            // _db.Categories.Add() tương đương với dbSet.Add()
        }
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }



        public void DeleteRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);

        }


        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter =null,string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(filter!=null){
            query = query.Where(filter);
}
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}