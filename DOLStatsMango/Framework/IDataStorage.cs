using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOLStatsMango.Framework
{
    public interface IDataStorage
    {
        bool Backup(string token, object value);
        bool DoesExist(string token);
        T Restore<T>(string token);
    }
}
