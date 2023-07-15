


namespace BLL.Base
{
    public class BaseBLL<T> : IBaseBLL<T> where T : class
    {
        protected IBaseDAL<T> _baseDal { get; set; }

        public BaseBLL()
        {
            _baseDal = new BaseDAL<T>();
        }

        #region DQL Sync
        public async Task<T> FirstOrDefaultSync(Expression<Func<T, bool>> expression)
        {
            return await _baseDal.FirstOrDefaultSync(expression);
        }

        async Task<IList<T>> IBaseBLL<T>.WhereSync(Expression<Func<T, bool>> expression) =>
           await _baseDal.WhereSync(expression);

        async Task<IList<T>> IBaseBLL<T>.WhereSync(Expression<Func<T, bool>> expressionFunc, Expression<Func<T, bool>> expressionSort) =>
            await _baseDal.WhereSync(expressionFunc, expressionSort);

        async Task<(IList<M>, int)> IBaseBLL<T>.WhereSync<M>(Expression<Func<T, bool>> expressionFunc, Expression<Func<T, bool>> expressionSort, Expression<Func<T, M>> expressionSelect, int page, int row) =>
            await _baseDal.WhereSync<M>(expressionFunc, expressionSort, expressionSelect, page, row);
        #endregion

        #region DML Sync
        async Task<bool> IBaseBLL<T>.AddRangeSync(IList<T> entities) =>
            await _baseDal.AddRangeSync(entities);
        async Task<bool> IBaseBLL<T>.AddSync(T entity) =>
            await _baseDal.AddSync(entity);
        async Task<bool> IBaseBLL<T>.DeleteSync(Expression<Func<T, bool>> expression) =>
            await _baseDal.DeleteSync(expression);
        async Task<bool> IBaseBLL<T>.UpdateSync(T newEntity) =>
            await _baseDal.UpdateSync(newEntity);
        #endregion

    }
}
