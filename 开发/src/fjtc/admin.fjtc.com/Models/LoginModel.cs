using System.ComponentModel.DataAnnotations;

namespace admin.fjtc.com.Models
{
    /// <summary>
    /// 登录模型
    /// </summary>
    public class LoginModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "验证码")]
        public string VerifyCode { set; get; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
       
        public long TimeTicks { set; get; }
    }
}