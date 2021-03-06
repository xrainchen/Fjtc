﻿using System;

namespace Fjtc.Model.ViewModel
{
    public class ProductUserViewModel:BaseViewModel
    {
        public long Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 登录名 数字和字母组合  必须同时包含数字和字母  6-18位
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadPhoto { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 绑定域名
        /// </summary>
        public string BindHost { get; set; }

        /// <summary>
        /// 发送红包密码
        /// </summary>
        public string SendRedPackPassword { get; set; }
    }
}
