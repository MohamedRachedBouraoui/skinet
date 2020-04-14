using System;
using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            inputQuery = HandleCriteria(inputQuery, spec);
            inputQuery = HandleOrderBy(inputQuery, spec);
            inputQuery = HandleIncludes(inputQuery, spec);
            inputQuery = HandlePaging(inputQuery, spec); //MUST BE LAST STEP

            return inputQuery;


        }

        private static IQueryable<T> HandlePaging(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            if (spec.IsPagingEnabled == true)
            {
                inputQuery = inputQuery.Skip(spec.Skip).Take(spec.Take);
            }
            return inputQuery;
        }

        private static IQueryable<T> HandleIncludes(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            // inputQuery = spec.Includes.Aggregate(inputQuery, (current, include) => current.Include(include));
            foreach (var include in spec.Includes)
            {
                inputQuery = inputQuery.Include(include);
            }

            return inputQuery;
        }

        private static IQueryable<T> HandleOrderBy(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            if (spec.OrderBy != null)
            {
                inputQuery = inputQuery.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                inputQuery = inputQuery.OrderByDescending(spec.OrderByDescending);
            }

            return inputQuery;
        }

        private static IQueryable<T> HandleCriteria(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            if (spec.Criteria != null)
            {
                inputQuery = inputQuery.Where(spec.Criteria);
            }

            return inputQuery;
        }
    }
}