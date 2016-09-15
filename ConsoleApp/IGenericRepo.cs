﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ConsoleApp
{
    public interface IGenericRepo<T> where T : class
    {
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        void Add(params T[] items);
        void Update(params T[] items);
        void Update(T item, Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        void Remove(params T[] items);
        void Dispose();
    }
}
