using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;
using TalabatCore.Repositories;
using TalabatRepository.Context;
using TalabatRepository.GenericRepository;

namespace TalabatRepository
{
    public class UnitOfWork : IUnitofwork
    {
        private Hashtable _repositores;
        private readonly StoreContext context;
        public UnitOfWork(StoreContext context)
        {
            this.context = context;
        }
        public async Task<int> Complete()
          => await context.SaveChangesAsync();
      

        public void Dispose()
        {
           context.Dispose();
        }



        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositores == null)
            {
                _repositores = new Hashtable();
            }
            var type = typeof(TEntity).Name;
            if (!_repositores.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(context);
                _repositores.Add(type, repository);
            }
            return (IGenericRepository<TEntity>)_repositores[type];

        }
    }
}
