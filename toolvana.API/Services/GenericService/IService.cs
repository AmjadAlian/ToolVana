﻿using System.Linq.Expressions;

namespace toolvana.API.Services.GenericService
{
    public interface IService <T> where T : class 
    {
       
        Task <IEnumerable <T>> GetAsync(Expression <Func<T,bool>>? filter = null , Expression <Func<T,object>> []? includes = null , bool isTracked = true);
        Task <T?> GetOneAsync(Expression<Func<T, bool>> expression , Expression <Func<T,object>> []? includes = null , bool isTracked = true);
       Task <T> AddAsync(T entity , CancellationToken cancellationToken = default);
        Task <bool> RemoveAsync(int id , CancellationToken cancellationToken = default);
    }
}
