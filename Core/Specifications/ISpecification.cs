using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        // This Criteria Defines What Out Where Condition 
        Expression<Func<T,bool>> Criteria { get; }
        List<Expression<Func<T,object>>> Includes { get; }
        // Predicate is the delegate like Func and Action delegates.
        // It represents a method containing a set of criteria and checks
        //whether the passed parameter meets those criteria
        Expression<Func<T, object>> OrderBy { get;}
        Expression<Func<T, object>> OrderByDescinding { get; }

        // Properties Required for Pagination
        int Take { get; }
        int Skip { get; }
        bool IsPaginationEnabled { get; }


    }
}
