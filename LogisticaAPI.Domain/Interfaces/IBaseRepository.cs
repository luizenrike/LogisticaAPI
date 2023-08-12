using LogisticaAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.Domain.Interfaces
{
    public interface IBaseRepository <T> where T : BaseEntity
    {
        List<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<int> CompleteAsync();

    }
}
