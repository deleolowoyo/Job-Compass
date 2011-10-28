using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Shell;

namespace DOLStatsMango.Framework
{
    public class TransientDataStorage : IDataStorage
    {
        public bool Backup(string token, object value)
        {
            if (null == value)
                return false;

            var store = PhoneApplicationService.Current.State;
            if (store.ContainsKey(token))
                store[token] = value;
            else
                store.Add(token, value);

            return true;
        }

        public bool DoesExist(string token)
        {
            var store = PhoneApplicationService.Current.State;

            return store.ContainsKey(token);
        }

        public T Restore<T>(string token)
        {
            var store = PhoneApplicationService.Current.State;
            if (!store.ContainsKey(token))
                return default(T);

            return (T)store[token];
        }

    }
}
