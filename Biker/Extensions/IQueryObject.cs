﻿namespace Biker.Extensions
{
    public interface IQueryObject
    {
        string SortBy { get; set; }

        bool IsSortAscending { get; set; }
    }
}
