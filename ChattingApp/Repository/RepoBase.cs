using ChattingApp.Contracts;
using ChattingApp.Models;
using ChattingApp.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ChattingApp.Repository
{
    public class RepoBase<T> : IRepoBase<T> where T : IBaseEntity
    {
        private readonly AppDbContext _dbContext;

        public RepoBase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAll() => await _dbContext.Set<T>().ToListAsync();


        public async Task<IReadOnlyList<T>> FindAllWithExpression(List<Expression<Func<T, bool>>> filters, List<Expression<T, object>>> includes = null)
        {
            IQueryable<T> query = _dbSet;

            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> FindByIdWithExpressions(List<Expression<Func<T, bool>>> filters, List<Expression<T, object>>> includes = null)
        {
            IQueryable<T> query = _dbContext;

            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetById(int id) => await _dbContext.Set<T>().FindAsync(id);

        public void Create(T entity) =>  _dbContext.Set<T>().AddAsync(entity);
        

        public void Delete(int id)
        {
            _dbContext.Remove(id);
        }

        public Task Save()
        {
            return _dbContext.SaveChangesAsync();
        }

        
    }
}
