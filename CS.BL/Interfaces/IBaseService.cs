using CS.DAL.Models;

namespace CS.BL.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetById(Guid id);
        Task<bool> Create(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(Guid id);
    }
}
