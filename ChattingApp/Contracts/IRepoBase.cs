using System.Linq.Expressions;

namespace ChattingApp.Contracts
{
    public interface IRepoBase<T>
    {
        Task<IReadOnlyList<T>> GetAll();
        Task<IReadOnlyList<T>> FindAllWithExpression(Expression<Func<T, bool>> filter);
        Task<T> FindByIdWithExpressions(List<Expression<Func<T, bool>>> filters);
        Task<T> GetById(int id);
        void Create(T entity);
        void Delete(int id);
        Task Save();
    }

}
