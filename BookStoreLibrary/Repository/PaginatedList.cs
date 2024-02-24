﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreLibrary.Repository
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;

            this.AddRange(items);
        }

       public static PaginatedList<T> Create(
    IQueryable<T> source, int pageIndex, int pageSize)
{
    var count = source.Count();
    var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
    return new PaginatedList<T>(items, count, pageIndex, pageSize);
}
    }
}
