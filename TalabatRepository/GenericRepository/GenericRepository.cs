using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;
using TalabatCore.Repositories;
using TalabatCore.Specification;
using TalabatRepository.Context;
using TalabatRepository.Context.Specification;

namespace TalabatRepository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext context;

        public GenericRepository(StoreContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id)
        => await context.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(Ispecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdWithSpecificaionAsync(Ispecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public IQueryable<T> ApplySpecification(Ispecification<T> spec)
        {
            //                               GetQuery(IQueryable<T> InputQuery,Ispecification spec)
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>(), spec);
        }

        public async Task<int> GetAcountAsync(Ispecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task CreateAsync(T entity)
            => await context.Set<T>().AddAsync(entity);

        public void Update(T entity)
            =>context.Set<T>().Update(entity);

        public void Delete(T entity)
            => context.Set<T>().Remove(entity);

      
    }
}
