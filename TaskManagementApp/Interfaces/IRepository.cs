using System.Linq.Expressions;

namespace TaskManagementApp.Interfaces
{
    public interface IRepository<T> where T : class
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Delete(Expression<Func<T, bool>> where);
        T GetById(int id);
        T Get(Expression<Func<T, bool>> where);
        ICollection<T> GetAll();
        ICollection<T> GetMany(Expression<Func<T, bool>> where);
    }
}
