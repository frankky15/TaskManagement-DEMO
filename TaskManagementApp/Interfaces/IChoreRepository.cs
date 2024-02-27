using System.Linq.Expressions;
using TaskManagementApp.Models;

namespace TaskManagementApp.Interfaces
{
    public interface IChoreRepository : IRepository<Chore>
    {
        bool Delete(Chore entity);
        bool Delete(Expression<Func<Chore, bool>> _where);
        bool Update(Chore entity);
    }
}
