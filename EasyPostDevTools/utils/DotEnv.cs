using System.Collections.Generic;

namespace EasyPostDevTools.utils
{
    using System;
    using System.IO;

    public class DotEnv
    {
        private Dictionary<String, String> _env = new Dictionary<string, string>();

        public void Load(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;
                
                _env.Add(parts[0], parts[1]);
            }
        }
        
        public String get(String key)
        {
            try
            {
                return _env[key];
            } catch (Exception e)
            {
                return null;
            }
        }
    }
}