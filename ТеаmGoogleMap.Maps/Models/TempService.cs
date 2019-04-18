using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ТеаmGoogleMap.Maps.Models
{
    public class TempService<T> : IGenericService<T> where T : class, new()
    {
        TeamGoogleMapContext context;
        IDbSet<T> entities;
        public TempService()
        {
            context = new TeamGoogleMapContext();
            entities = context.Set<T>();
        }
        public T Add(T obj)
        {
            throw new NotImplementedException();
        }

        public T Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return entities.Where(predicate);
        }

        public T Get(int id)
        {
            return entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities;
        }

        public T Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}