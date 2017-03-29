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
        IList<T> Get(T entity);
    }

    /// <summary>
    /// 唯一标识层
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IIdentityDal<T> : IEntityDal<T> where T : IEntity
    {
        T Get(long id);
        bool Delete(long id);
        bool Exist(long id);
    }
    #endregion


    #region 抽象层

    public abstract class EntityDal<T> : IEntityDal<T> where T : IEntity
    {
        public abstract bool Add(T entity);

        public abstract bool Update(T entity);

        public abstract IList<T> Get(T entity);
    }

    public abstract class IdentityDal<T> : EntityDal<T>, IIdentityDal<T> where T : IIdentityEntity
    {
        public abstract T Get(long id);

        public abstract bool Delete(long id);

        public abstract bool Exist(long id);
    }
    #endregion
}
