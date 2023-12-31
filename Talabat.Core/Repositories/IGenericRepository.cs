using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;
using TalabatCore.Specification;

namespace TalabatCore.Repositories
{
    public interface IGenericRepository<T>where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(Ispecification<T> spec);
        Task<T> GetByIdWithSpecificaionAsync(Ispecification<T> spec);
        IQueryable<T> ApplySpecification(Ispecification<T> spec);
        Task<int> GetAcountAsync(Ispecification<T> spec);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
