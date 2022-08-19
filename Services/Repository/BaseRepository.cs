using Metrosoft.Context;
using Metrosoft.Services.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Metrosoft.Services.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void DeleteUser(int id)
        {
            var deleteuser = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            _context.Users.Remove(deleteuser);
        }

       

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includePropperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            //includePropperties will be coma seperated
            if (includePropperties != null)
            {
                foreach (var includePropperty in includePropperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includePropperty);
                }
            }

            return query.FirstOrDefault();
        }

     

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
