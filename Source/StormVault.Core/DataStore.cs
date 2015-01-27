using Newtonsoft.Json;
using System.IO;

namespace StormVault.Core
{
    public class DataStore
    {
        private static object fileSync = new object();

        public static void Store(object item, string fileName)
        {
            lock (fileSync)
                File.WriteAllText(fileName, JsonConvert.SerializeObject(item));
        }

        public static T Fetch<T>(string fileName)
        {
            if (!File.Exists(fileName))
                return default(T);

            lock (fileSync)
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
        }
    }
}
