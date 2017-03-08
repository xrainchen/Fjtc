﻿using System.Web.Mvc;

namespace admin.fjtc.com.Common
{
    /// <summary>
    /// DWZ框架帮助类
    /// </summary>
    public static class DwzHelper
    {
        public static ActionResult SuccessAndClose(string navTab, string msg = "")
        {
            var json = new JsonResult()
            {
                Data = new
                {
                    statusCode = "200",
                    message = msg,
                    navTableId = navTab,
                    callbackType = "closeCurrent",
                    forwardUrl = ""
                }
            };
            return json;
        }
    }
}