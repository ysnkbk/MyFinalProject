using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using System.Linq.Expressions;
using Entities.Abstract;    



namespace DataAccess.Abstract
{
    // generic constraint 
    // class: referans tip
    // IEntity  ientity olabilir ya da onu implement eden sınıflar olabilir
    // new() : interfaceler newlenemez somut nesneler
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        T Get(Expression<Func<T,bool>> filter);

        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
