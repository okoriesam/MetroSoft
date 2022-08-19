using System.Linq.Expressions;

namespace Metrosoft.Services.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        //IEnumerable<T> GetAllUsers();
        void Add(T entity);

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
             string includePropperties = null
           );

        void DeleteUser(int id);
        void Save();
    }
}
