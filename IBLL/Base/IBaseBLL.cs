using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL.Base
{
    public interface IBaseBLL<T> where T : class
    {

        #region DQL Sync

        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<T> FirstOrDefaultSync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<IList<T>> WhereSync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 查询集合并排序
        /// </summary>
        /// <param name="expressionFunc">查询条件</param>
        /// <param name="expressionSort">排序条件</param>
        /// <returns></returns>
        Task<IList<T>> WhereSync(Expression<Func<T, bool>> expressionFunc, Expression<Func<T, bool>> expressionSort);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="expressionFunc"></param>
        /// <param name="expressionSort"></param>
        /// <param name="expressionReturn"></param>
        /// <param name="page"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        Task<(IList<M>, int)> WhereSync<M>(Expression<Func<T, bool>> expressionFunc, Expression<Func<T, bool>> expressionSort, Expression<Func<T, M>> expressionSelect, int page, int row);
        #endregion

        #region DML Sync
        /// <summary>
        /// 新增一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> AddSync(T entity);
        /// <summary>
        /// 新增实体集合
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<bool> AddRangeSync(IList<T> entities);
        /// <summary>
        /// 删除实体集合
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> DeleteSync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 更新一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newEntity"></param>
        /// <returns></returns>
        Task<bool> UpdateSync(T newEntity);
        #endregion

    }
}
