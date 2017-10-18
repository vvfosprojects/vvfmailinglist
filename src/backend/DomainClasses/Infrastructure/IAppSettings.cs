using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Infrastructure
{
    public interface IAppSettings
    {
        string ConnectionString { get; }
    }
}