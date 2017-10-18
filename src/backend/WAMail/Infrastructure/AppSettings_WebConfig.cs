using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using DomainClasses.Infrastructure;

namespace WAMail.Infrastructure
{
    public class AppSettings_WebConfig : IAppSettings
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }
    }
}