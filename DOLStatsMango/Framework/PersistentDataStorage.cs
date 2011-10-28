using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.Text;

namespace DOLStatsMango.Framework
{
    public class PersistentDataStorage : IDataStorage
    {
        public bool Backup(string token, object value)
        {
            if (null == value)
                return false;

            var store = IsolatedStorageSettings.ApplicationSettings;
            if (store.Contains(token))
                store[token] = Serialize(value);
            else
                store.Add(token, Serialize(value));

            store.Save();
            return true;
        }

        public bool DoesExist(string token)
        {
            var store = IsolatedStorageSettings.ApplicationSettings;

            return store.Contains(token);
        }

        public T Restore<T>(string token)
        {
            var store = IsolatedStorageSettings.ApplicationSettings;
            if (!store.Contains(token))
                return default(T);

            return Deserialize<T>(store[token].ToString());
        }

        public void Delete(string token)
        {
            IsolatedStorageSettings.ApplicationSettings.Remove(token);
        }

        private static string Serialize(object objectToSerialize)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(objectToSerialize.GetType());
                serializer.WriteObject(ms, objectToSerialize);
                ms.Position = 0;

                using (StreamReader reader = new StreamReader(ms))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static T Deserialize<T>(string jsonString)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }

        /*
        public bool Backup(string token, object value)
        {
            if (null == value)
                return false;

            var store = IsolatedStorageSettings.ApplicationSettings;
            if (store.Contains(token))
                store[token] = value;
            else
                store.Add(token, value);

            store.Save();
            return true;
        }

        public bool DoesExist(string token)
        {
            var store = IsolatedStorageSettings.ApplicationSettings;

            return store.Contains(token);
        }

        public T Restore<T>(string token)
        {
            var store = IsolatedStorageSettings.ApplicationSettings;
            if (!store.Contains(token))
                return default(T);

            return (T)store[token];
        }

        public void Delete(string token)
        {
            IsolatedStorageSettings.ApplicationSettings.Remove(token);
        }
         
        public static void UpdateOrAdd(this IsolatedStorageSettings obj, string key, object value)
        {
          if (obj.Contains(key))
          {
            obj[key] = value;
          }
          else
          {
            obj.Add(key, value);
          }
        }


        private static string Serialize(object objectToSerialize)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(objectToSerialize.GetType());
                serializer.WriteObject(ms, objectToSerialize);
                ms.Position = 0;

                using (StreamReader reader = new StreamReader(ms))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static T Deserialize<T>(string jsonString)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }
         * 
         * 
         * 
         * 

        private static IsolatedStorageFile _isoStore;
        public static IsolatedStorageFile IsoStore
        {
            get { return _isoStore ?? (_isoStore = IsolatedStorageFile.GetUserStoreForApplication()); }
        }

        public static void SaveList<T>(string folderName, string dataName, ObservableCollection<T> dataList) where T : class
        {
            if (!IsoStore.DirectoryExists(folderName))
            {
                IsoStore.CreateDirectory(folderName);
            }

            string fileStreamName = string.Format("{0}\\{1}.dat", folderName, dataName);

            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileStreamName, FileMode.Create, IsoStore))
            {
                DataContractSerializer dcs = new DataContractSerializer(typeof(ObservableCollection<T>));
                dcs.WriteObject(stream, dataList);
            }
        }

        public static ObservableCollection<T> LoadList<T>(string folderName, string dataName) where T : class
        {
            ObservableCollection<T> retval = new ObservableCollection<T>();

            string fileStreamName = string.Format("{0}\\{1}.dat", folderName, dataName);

            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileStreamName, FileMode.OpenOrCreate, IsoStore))
            {
                if (stream.Length > 0)
                {
                    DataContractSerializer dcs = new DataContractSerializer(typeof(ObservableCollection<T>));
                    retval = dcs.ReadObject(stream) as ObservableCollection<T>;
                }
            }

            return retval;
        }
         * */

    }
}
