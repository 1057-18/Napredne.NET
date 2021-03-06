﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmploymentWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploymentWebApp
{
    public class PaginatedList<T> : List<T>
    {
        static public int PageIndex { get; private set; }
        static public int TotalPages { get; private set; }
        static public string SortOrder { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, string sortOrder)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            SortOrder = sortOrder;

            AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize, string sortOrder)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize, sortOrder);
        }
    }
}
