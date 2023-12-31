using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;
using TalabatCore.Specification;

namespace TalabatRepository.Context.Specification
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery,Ispecification<T> spec)
        {
            var query = InputQuery;
            if (spec.IsPaginationEnable)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrederByDec != null)
            {
                query = query.OrderByDescending(spec.OrederByDec);
            }
            if (spec.Criteria != null)
            {
                query= query.Where(spec.Criteria);
            }
            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            return query;
            //context.Set<T>().orderBy(p=p.Name).Where(p=>p.Id).Include(p=>p.ProductBrand);
        }
    }
}
