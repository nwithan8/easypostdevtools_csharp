using System;
using System.Collections.Generic;
using EasyPostDevTools.models;

namespace EasyPostDevTools
{
    using utils;
    
    public class Constants
    {
        public static string CUSTOMS_ITEMS_JSON = "json/customs_items.json";
        public static string CUSTOMS_INFO_JSON = "json/customs_info.json";
        public static string CARRIERS_JSON = "json/carriers.json";
        public static string LABEL_OPTIONS_JSON = "json/label_options.json";
        public static string TRACKERS_JSON = "json/trackers.json";
        public static string OPTIONS_JSON = "json/options.json";
        public static string PICKUPS_JSON = "json/pickups.json";

        private static Dictionary<Addresses.State.STATE, JsonAddressFile> _stateDictionary =
            new Dictionary<Addresses.State.STATE, JsonAddressFile>();

        private static Dictionary<Addresses.Country.COUNTRY, JsonAddressFile> _countryDictionary =
            new Dictionary<Addresses.Country.COUNTRY, JsonAddressFile>();

        private class JsonFile
        {
            private string FileName;
            private string ParentFolder;

            public JsonFile(string fileName, string parentFolder)
            {
                FileName = fileName;
                ParentFolder = parentFolder;
            }

            public string JsonPath
            {
                get => $"{ParentFolder}/{FileName}.min.json";
            }
        }

        private class JsonAddressFile : JsonFile
        {
            public JsonAddressFile(string abbreviation, string parentFolder) : base(abbreviation, parentFolder)
            {
            }

            public string AddressFile
            {
                get => $"json/addresses/{JsonPath}";
            }
        }

        public static class Addresses
        {
            public class Country : ExtendedEnum
            {
                public enum COUNTRY
                {
                    UNITED_STATES,
                    CANADA,
                    CHINA,
                    HONG_KONG,
                    UNITED_KINGDOM,
                    GERMANY,
                    SPAIN,
                    MEXICO,
                    AUSTRALIA
                }

                public static List<COUNTRY> Values
                {
                    get => Values<COUNTRY>();
                }

                public static int Amount
                {
                    get => Values.Count;
                }

                public static List<COUNTRY> All
                {
                    get => Values;
                }

                public static COUNTRY GetRandom()
                {
                    var values = Values;
                    return values[utils.Random.RandomIntInRange(0, Values.Count - 1)];
                }
            }

            public class State : ExtendedEnum
            {
                public enum STATE
                {
                    ARIZONA,
                    CALIFORNIA,
                    IDAHO,
                    KANSAS,
                    NEVADA,
                    NEW_YORK,
                    OREGON,
                    TEXAS,
                    UTAH,
                    WASHINGTON
                }
                
                public static List<STATE> Values
                {
                    get => Values<STATE>();
                }

                public static int Amount
                {
                    get => Values.Count;
                }
                
                public static List<STATE> All
                {
                    get => Values;
                }
                
                public static STATE GetRandom()
                {
                    var values = Values;
                    return values[utils.Random.RandomIntInRange(0, Values.Count - 1)];
                }
                
            }
            
            private static JsonAddressFile GetJsonAddressFile(Country.COUNTRY country)
            {
                try
                {
                    return _countryDictionary[country];
                }
                catch (Exception)
                {
                    return null;
                }
            }

            private static JsonAddressFile GetJsonAddressFile(State.STATE state)
            {
                try
                {
                    return _stateDictionary[state];
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static string GetAddressFile(Country.COUNTRY country)
            {
                return GetJsonAddressFile(country)?.AddressFile;
            }

            public static string GetAddressFile(State.STATE state)
            {
                return GetJsonAddressFile(state)?.AddressFile;
            }

            public static string GetRandomStateAddressFile()
            {
                var state = State.GetRandom();
                return GetAddressFile(state);
            }
            
            public static string GetRandomCountryAddressFile()
            {
                var country = Country.GetRandom();
                return GetAddressFile(country);
            }
            
            public static string GetRandomAddressFile()
            {
                return Random.RandomBool ? GetRandomCountryAddressFile() : GetRandomStateAddressFile();
            }
        }
    }
}