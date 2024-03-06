using System.Linq.Expressions;

namespace TaskManagementApp.Interfaces
{
    public interface IRepository<T> where T : class
    {
        bool Add(T entity);
        T GetById(int id);
        T Get(Expression<Func<T, bool>> _where);
        ICollection<T> GetAll();
        ICollection<T> GetMany(Expression<Func<T, bool>> _where);
        bool IdExists(int id);
    }
}
