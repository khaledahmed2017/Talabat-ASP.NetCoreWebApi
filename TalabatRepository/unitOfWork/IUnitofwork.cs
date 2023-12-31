using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;
using TalabatCore.Repositories;

namespace TalabatRepository
{
    public interface IUnitofwork:IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity:BaseEntity;
        Task<int> Complete();
    }
}
