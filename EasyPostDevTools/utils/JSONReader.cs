using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace EasyPostDevTools.utils
{
    public class JSONReader
    {
        private static string GetFilePathFromResources(string filename)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $@"{filename}");
        }

        private static List<Dictionary<string, object>> ReadJsonFileJson(string path)
        {
            try
            {
                var filePath = GetFilePathFromResources(path);
                var json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private static List<object> ReadJsonFileArray(string path)
        {
            try
            {
                var filePath = GetFilePathFromResources(path);
                var json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<object>>(json);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<Dictionary<string, object>> GetRandomMapsFromJsonFile(string path, int amount,
            bool allowDuplicates)
        {
            var data = ReadJsonFileJson(path);
            var maps = new List<Dictionary<string, object>>();
            foreach (Dictionary<string, object> map in Random.RandomItemsFromList(data, amount, allowDuplicates))
            {
                maps.Add(map);
            }

            return maps;
        }

        public static List<object> GetRandomItemsFromJsonFile(string path, int amount, bool allowDuplicates)
        {
            var data = ReadJsonFileArray(path);
            return Random.RandomItemsFromList(data, amount, allowDuplicates);
        }
    }
}