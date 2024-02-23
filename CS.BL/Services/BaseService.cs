using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationContext _context;
        public BaseService(ApplicationContext context)
        {
            _context = context;
        }

        protected async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();

            return saved > 0 ? true : false;
        }

        public async Task<bool> Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);

            return await SaveAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(u => u.Id == id);

            _context.Set<TEntity>().Remove(entity);

            return await SaveAsync();
        }

        public async Task<TEntity?> GetById(Guid id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);

            return await SaveAsync();
        }
    }
}
