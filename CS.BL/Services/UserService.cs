using CS.DAL.DataAccess;

namespace CS.BL.Services
{
    public class UserService
    {
        private readonly ApplicationContext _context;
        public UserService()
        {
            _context = new ApplicationContext();
        }


    }
}
