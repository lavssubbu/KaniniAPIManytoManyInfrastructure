using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIManytoManyKaniniSln.Application
{
    public interface IUserPost<TEntity,TKey> where TEntity : class
                                              where TKey : notnull,IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(TKey id);
        Task<TEntity> Add(TEntity entity);
        Task Update(TKey id,TEntity entity);

    }

}
