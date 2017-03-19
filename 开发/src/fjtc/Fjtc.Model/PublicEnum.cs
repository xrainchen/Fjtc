namespace Fjtc.Model
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserTypeEnum
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        Administrator = 0,
        /// <summary>
        /// 用户
        /// </summary>
        User = 1
    }
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatusEnum
    {
        /// <summary>
        /// 注销
        /// </summary>
        Cancel = -1,
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1
    }
    /// <summary>
    /// CMS菜单类型枚举
    /// </summary>
    public enum CmsMenuTypeEnum
    {
        Folder = 1,
        Menu = 2,
        Button = 3
    }
}
