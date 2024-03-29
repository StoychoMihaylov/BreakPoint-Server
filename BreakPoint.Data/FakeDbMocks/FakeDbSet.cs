﻿namespace BreakPoint.Data.FakeDbMocks
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class FakeDbSet<T> : DbSet<T>, IQueryable, IEnumerable<T> where T : class
    {
        protected HashSet<T> _set;
        protected IQueryable _query;

        public FakeDbSet()
        {
            this._set = new HashSet<T>();
            this._query = this._set.AsQueryable();
        }

        public T Add(T entity)
        {
            this._set.Add(entity);
            return entity;
        }

        public T Remove(T entity)
        {
            this._set.Remove(entity);
            return entity;
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return this._query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return this._query.Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._set.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this._set.GetEnumerator();
        }
    }
}
