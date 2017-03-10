using System;

namespace Fjtc.Model.ViewModel
{
    public class UserViewModel:BaseViewModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        public string StatusName
        {
            get
            {
                switch (Status)
                {
                    case -1:
                        return "注销";
                    case 1:
                        return "正常";
                }
                return "异常";
            }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 用户类型  0：超级管理员 1：操作员
        /// </summary>
        public UserTypeEnum UserType { get; set; }
    }
}
