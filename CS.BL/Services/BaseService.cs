using CS.DAL.DataAccess;

namespace CS.BL.Services
{
    public abstract class BaseService
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
    }
}
