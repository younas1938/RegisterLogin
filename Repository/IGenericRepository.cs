using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Dto;
using UserEntity.Models;

namespace UserEntity.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
       
      
        bool Add(T model);
        bool Update(T model);
        T GetById(int id);
        IEnumerable<T> GetAll(UserParameter userParameter);
        bool Delete(T model);
        void Save();
        IEnumerable<T> Search(string name);


    }
}
