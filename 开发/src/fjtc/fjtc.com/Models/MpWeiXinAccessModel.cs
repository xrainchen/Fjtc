using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fjtc.com.Models
{
    public class MpWeiXinAccessModel
    {

        public string Signature { get; set; }

        public string Timestamp { get; set; }

        public string Nonce { get; set; }

        public string Echostr { get; set; }
    }
}