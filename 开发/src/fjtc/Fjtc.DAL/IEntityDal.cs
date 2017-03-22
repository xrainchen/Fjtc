using System.Collections.Generic;
using Fjtc.Model.Entity;

namespace Fjtc.DAL
{
    #region 接口层
    /// <summary>
    /// 数据访问层基本接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityDal<T> where T : IEntity
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        IList<T> Get(T entity);
    }

    /// <summary>
    /// 唯一标识层
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IIdentityDal<T> : IEntityDal<T> where T : IEntity
    {
        T Get(long id);
    }
    #endregion


    #region 抽象层

    public abstract class EntityDal<T> : IEntityDal<T> where T : IEntity
    {
        public virtual bool Add(T entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual bool Update(T entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual bool Delete(T entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual IList<T> Get(T entity)
        {
            throw new System.NotImplementedException();
        }
    }

    public abstract class IdentityDal<T> : EntityDal<T>, IIdentityDal<T> where T : IEntity
    {
        public virtual T Get(long id)
        {
            throw new System.NotImplementedException();
        }
    }
    #endregion
}
