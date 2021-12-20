using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyPostDevTools.models
{
    public class Mapper
    {
        public static JObject toJson(object obj)
        {
            return JObject.Parse(JsonConvert.SerializeObject(obj));
        }

        public static Dictionary<String, Object> toMap(object obj)
        {
            var json = toJson(obj);
            var map = new Dictionary<String, Object>();
            foreach (var prop in json)
            {
                map.Add(prop.Key, prop.Value);
            }

            return map;
        }
    }
}