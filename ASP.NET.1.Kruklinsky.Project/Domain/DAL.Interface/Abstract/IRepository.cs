﻿using System.Collections.Generic;
using System.Linq;

namespace DAL.Interface.Abstract
{
    public interface IRepository<T>
    {
        IEnumerable<T> Data { get; }

        void Add(T item);

        void Delete(T item);

        void Update(T item);
    }
}