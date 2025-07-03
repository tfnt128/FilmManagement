using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmManagement.Shared.Data.Data
{
    public class DAL<T> where T : class
    {

        private readonly FilmManagementContext _context;

        public DAL(FilmManagementContext context)
        {
            _context = context;
        }
        public IEnumerable<T> List()
        {
            return _context.Set<T>().ToList();
        }
        public void Add(T obj)
        {
            _context.Set<T>().Add(obj);
            _context.SaveChanges();
        }
        public void Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
            _context.SaveChanges();
        }
        public void Update(T obj)
        {
            _context.Set<T>().Update(obj);
            _context.SaveChanges();
        }
        public T? Recover(Func<T, bool> condition)
        {
            return _context.Set<T>().FirstOrDefault(condition);
        }
    }
}
