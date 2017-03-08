using System;
using System.Drawing;

namespace Fjtc.Common
{
    /// <summary>
    /// 操作类
    /// </summary>
    public static class Operator
    {
        #region 生成随即验证码
        /// <summary>
        /// 根据随机数生成验证码
        /// </summary>
        /// <param name="checkCode">随机数</param>
        public static void CreateImage(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 11);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 19);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);
            //定义颜色
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Chocolate, Color.Brown, Color.DarkCyan, Color.Purple };
            Random rand = new Random();

            //输出不同字体和颜色的验证码字符
            for (int i = 0; i < checkCode.Length; i++)
            {
                int cindex = rand.Next(7);
                Font f = new System.Drawing.Font("Microsoft Sans Serif", 11);
                Brush b = new System.Drawing.SolidBrush(c[cindex]);
                g.DrawString(checkCode.Substring(i, 1), f, b, (i * 10) + 1, 0, StringFormat.GenericDefault);
            }
            //画一个边框
            g.DrawRectangle(new Pen(Color.Black, 0), 0, 0, image.Width - 1, image.Height - 1);

            //输出到浏览器
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            System.Web.HttpContext.Current.Response.ClearContent();
            System.Web.HttpContext.Current.Response.ContentType = "image/Jpeg";
            System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
        }

        /// <summary>
        /// 获得随机数
        /// </summary>
        /// <param name="num">几位数</param>
        /// <returns>随机数</returns>
        public static string GetValidateCode(int num)
        {
            char[] chars = "0123456789".ToCharArray();
            var random = new Random();
            string validateCode = string.Empty;
            for (int i = 0; i < num; i++)
            {
                char rc = chars[random.Next(0, chars.Length)];
                if (validateCode.IndexOf(rc) > -1)
                {
                    i--;
                    continue;
                }
                validateCode += rc;
            }
            return validateCode;
        }
        #endregion
    }
}
