using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
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

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = 
            new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDesc { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnable { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpession)
        {
            Includes.Add(includeExpession);
        }
        protected void AddOrderby(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy=orderByExpression;
        }
        protected void AddOrderbyDesc(Expression<Func<T, object>> orderByExpressionDesc)
        {
            OrderByDesc = orderByExpressionDesc;
        }
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnable = true;
        }
    }
}
