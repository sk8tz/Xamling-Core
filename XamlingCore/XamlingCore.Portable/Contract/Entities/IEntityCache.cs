﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamlingCore.Portable.Contract.Entities
{
    public interface IEntityCache
    {
        Task<T> GetEntity<T>(string key, Func<Task<T>> sourceTask, TimeSpan? maxAge = null,
            bool allowExpired = true, bool allowZeroList = true) where T : class, new();
        Task<T> GetEntity<T>(string key) where T : class, new();
        Task<bool> SetEntity<T>(string key, T item) where T : class, new();
        Task<bool> SetEntity<T>(string key, T item, TimeSpan? maxAge) where T : class, new();
       
        Task<bool> Delete<T>(string key) where T : class, new();
    }

    public interface IFileEntityCache : IEntityCache
    {

        Task<List<T>> GetAll<T>()
            where T : class, new();

        Task DeleteAll<T>()
            where T : class, new();

        Task<TimeSpan?> GetAge<T>(string key) where T : class, new();

        Task<bool> ValidateAge<T>(string key)
            where T : class, new();

        Task Clear();
    }

    public interface ILegacyEntityCache : IFileEntityCache
    {
        Task DisableMemoryCache();
        Task EnableMemoryCache();
    }
}