using Fjtc.Model;

namespace fjtc.com.Common
{
    public class JsonResultHelper
    {
        public static ReturnJsonResultModel Success(string msg="")
        {
            return new ReturnJsonResultModel()
            {
                ReturnData = null,
                ReturnCode = 200,
                Message = msg
            };
        }

        public static ReturnJsonResultModel Warn(string msg = "")
        {
            return new ReturnJsonResultModel()
            {
                ReturnData = null,
                ReturnCode = 300,
                Message = msg
            };
        }
    }
}