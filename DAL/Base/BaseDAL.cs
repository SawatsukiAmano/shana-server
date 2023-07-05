using DBUtility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Base
{
    public class BaseDAL<T> : IBaseDAL<T> where T : class
    {

        private readonly EFPostgreSqlContext _context;
        public BaseDAL()
        {
            _context = new EFPostgreSqlContext();
        }

        #region DQL Sync

        Task<T> IBaseDAL<T>.FirstOrDefaultSync(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        async Task<IList<T>> IBaseDAL<T>.WhereSync(Expression<Func<T, bool>> expression) => await Task.Run(() =>
        {
            return _context.Set<T>().Where(expression).ToList();
        });

        public async Task<IList<T>> WhereSync(Expression<Func<T, bool>> expressionFunc, Expression<Func<T, bool>> expressionSort) => await Task.Run(() =>
        {
            return _context.Set<T>().Where(expressionFunc).OrderBy(expressionSort).ToList();
        });

        public async Task<(IList<M>, int)> WhereSync<M>(Expression<Func<T, bool>> expressionFunc, Expression<Func<T, bool>> expressionSort, Expression<Func<T, M>> expressionSelect, int page, int row) => await Task.Run(() =>
        {
            int count = _context.Set<T>().Count(expressionFunc);
            return (_context.Set<T>().Where(expressionFunc).OrderBy(expressionSort).Select(expressionSelect).Skip((page - 1) * row).Take(row).ToList(), count);
        });
        #endregion


        #region DML Sync


        public async Task<bool> UpdateSync(T newEntity) => await Task.Run(() =>
        {
            _context.Entry(newEntity).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        });
        public async Task<bool> AddSync(T entity) => await Task.Run(() =>
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges() > 0;
        });

        public async Task<bool> AddRangeSync(IList<T> entities) => await Task.Run(() =>
        {
            _context.Set<T>().AddRange(entities);
            return _context.SaveChanges() > 0;
        });

        public async Task<bool> DeleteSync(Expression<Func<T, bool>> expression) => await Task.Run(() =>
        {
            var removes = _context.Set<T>().Where(expression);
            _context.RemoveRange(removes);
            return _context.SaveChanges() > 0;
        });

        #endregion
    }
}
