namespace Fjtc.Model.Entity
{
    /// <summary>
    /// 数据库表接口
    /// </summary>
    public interface IEntity
    {

    }

    public interface IIdentityEntity : IEntity
    {
        long Id { get; set; }
    }
}
