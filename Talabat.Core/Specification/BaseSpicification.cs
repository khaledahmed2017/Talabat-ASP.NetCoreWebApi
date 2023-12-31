using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;

namespace TalabatCore.Specification
{
    public class BaseSpecification<T> : Ispecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T,object>> OrderBy { get; set; }
        public Expression<Func<T,object>> OrederByDec { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnable { get; set; }
        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public void AddOrderBy(Expression<Func<T,object>> orderByExprission)
        {
            OrderBy = orderByExprission;
        }
        public void AddOrderByDec(Expression<Func<T, object>> orderByExprission)
        {
            OrederByDec = orderByExprission;
        }
    }
}
