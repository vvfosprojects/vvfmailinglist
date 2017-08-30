using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAMail.Infrastructure
{
    public class Result
    {
        public bool ok { get; set; }

        public string errorCode { get; set; }

        public string errorMsg { get; set; }

        public Result(bool esito)
        {
            this.ok = esito;
            this.errorCode = string.Empty;
            this.errorMsg = string.Empty;
        }

        public void CreateResultException(Exception e)
        {
            this.errorCode = string.Format("{0}", e.HResult);
            this.errorMsg = string.Format("{0}", e.InnerException.Message);
        }
    }
}