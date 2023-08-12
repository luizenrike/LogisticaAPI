using LogisticaAPI.Domain.Interfaces;
using LogisticaAPI.Domain.Models;
using LogisticaAPI.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.Infra.Repository
{
    public class BaseRepository <T> : IBaseRepository<T> where T : BaseEntity
    {
        public readonly UnitOfWork dataBase;
        public BaseRepository(UnitOfWork context)
        {
            dataBase = context;
        }
        public List<T> GetAll()
        {
            return dataBase.Set<T>().ToList();
        }

        public void Insert(T entity)
        {
            dataBase.Set<T>().Add(entity);
            dataBase.SaveChanges();
        }

        public void Update(T entity)
        {
            dataBase.Set<T>().Update(entity);
            dataBase.SaveChanges();
        }

        public void Remove(T entity)
        {
            dataBase.Set<T>().Remove(entity);
            dataBase.SaveChanges();
        }
        public async Task<int> CompleteAsync()
        {
            return await dataBase.SaveChangesAsync(default);
        }
    }
}
