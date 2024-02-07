using System.Linq.Expressions;

namespace ChattingApp.Contracts
{
    public interface IRepoBase<T>
    {
        Task<IReadOnlyList<T>> GetAll();
        Task<T> FindAllWithExpression(List<Expression<Func<T, bool>>> filters, List<Expression<T, object>>> includes = null);
        Task<T> FindByIdWithExpressions(List<Expression<Func<T, bool>>> filters, List<Expression<T, object>>> includes = null);
        Task<T> GetById(int id);
        void Create(T entity);
        void Delete(int id);
        Task Save();
    }
}
