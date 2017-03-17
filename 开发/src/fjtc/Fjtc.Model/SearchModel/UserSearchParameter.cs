namespace Fjtc.Model.SearchModel
{
    /// <summary>
    /// 用户搜索参数模型
    /// </summary>
    public class UserSearchParameter : SearchParameter
    {
        public string Name { get; set; }
        public string MobilePhone { get; set; }

        public string LoginName { get; set; }

        public string BindHost { get; set; }
    }
}
