﻿using RPoney;
using System;

namespace Fjtc.Model.Entity
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Serializable]
    public class User
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
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 获取加密的密码 todo:
        /// </summary>
        public string EncryPassword(string password)
        {
            return $"{password}{PublicConst.SystemMainKey}".Md5();
        }
        /// <summary>
        /// 用户类型  0：超级管理员 1：操作员
        /// </summary>
        public UserTypeEnum UserType { get; set; }
    }
}
