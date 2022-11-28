using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Context;
using UserEntity.Dto;
using UserEntity.Helpers;
using UserEntity.Models;

namespace UserEntity.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T:class
    {
        private UserDbContext _context;
        //DbSet<T> _entity = null;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(UserDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll(UserParameter userParameter)
        {

            var data = (IEnumerable<T>)_context.GetUsers.OrderBy(x => x.UserName);
            if (data != null)
            {
                return PagedList<T>.ToPagedList(data, userParameter.PageNumber);
            }
            return null;


        }
        public IEnumerable<T> Search(string searchFilter)
        {
           var query1 = (IEnumerable<T>)_context.GetUsers.Where(x=>x.UserName.Contains(searchFilter)  || x.Email.Contains(searchFilter));

            if (!string.IsNullOrEmpty(searchFilter))
            {
                return PagedList<T>.ToPagedList(query1);
            }
            return null;
            
        }
        public bool Delete(T model)
        {
            try
            {
                _dbSet.Remove(model);
                _context.SaveChanges();
                return true;

            }
            catch (Exception )
            {

                return false;
            }
        }

       


        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public bool Update(T model)
        {
            try
            {
                _dbSet.Update(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public void Save()
        {
            _context.SaveChanges();
        }
          public bool Add(T model)
        {
            try
            {
                _dbSet.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}



//public virtual void Delete(string id)
//{
//    T entityToDelete = dbSet.Find(id);
//    Delete(entityToDelete);
//}
//public virtual void Delete(T entityToDelete)
//{
//    if (_db.Entry(entityToDelete).State == EntityState.Detached)
//    {
//        dbSet.Attach(entityToDelete);
//    }
//    dbSet.Remove(entityToDelete);
//}