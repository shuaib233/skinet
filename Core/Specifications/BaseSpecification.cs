using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria  {get;}

        public List<Expression<Func<T, object>>> Includes {get;} = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderByAsc  {get;private set;}

        public Expression<Func<T, object>> OrderByDesc {get;private set;}

        public int Take {get;private set;}

        public int Skip {get;private set;}

        public bool isPagingEnabled {get;private set;}

        protected void AddOrderByAsc(Expression<Func<T, object>> orderbyExp)
        {
           OrderByAsc = orderbyExp;
        }


        protected void AddOrderByDesc(Expression<Func<T, object>> orderbyExp)
        {
           OrderByDesc = orderbyExp;
        }

        protected void AddInclude(Expression<Func<T, object>> includeExp)
        {
            Includes.Add(includeExp);
        }

         protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            isPagingEnabled = true;
        }
    }
}