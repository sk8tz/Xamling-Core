﻿using System;
using System.Threading.Tasks;

namespace XamlingCore.Portable.Contract.Cache
{
    public interface IEntityCache
    {
        Task<T> GetEntity<T>(string key, Func<Task<T>> sourceTask, TimeSpan? maxAge = null, bool allowExpired = true, bool allowZeroList = true) where T : class, new();
        Task<T> GetEntity<T>(string key, TimeSpan? maxAge = null) where T : class, new();
        Task<bool> SetEntity<T>(string key, T item) where T : class, new();
        Task Clear();
        Task<bool> Delete<T>(string key) where T : class, new();

        void DisableMemoryCache();
        void EnableMemoryCache();
    }
}