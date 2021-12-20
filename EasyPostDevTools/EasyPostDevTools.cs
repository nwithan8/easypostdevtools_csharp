using System;
using System.Collections.Generic;
using EasyPost;
using EasyPostDevTools.models;
using EasyPostDevTools.utils;
using SimpleJson;
using Random = EasyPostDevTools.utils.Random;

namespace EasyPostDevTools
{
    public class EasyPostDevTools
    {
        public enum KeyType
        {
            TEST,
            PRODUCTION
        }

        public static void SetupKey(string key)
        {
            ClientManager.SetCurrent(key);
        }

        public static void SetupKey(string envDir, KeyType type)
        {
            var dotEnv = new DotEnv();
            dotEnv.Load(envDir);
            switch (type)
            {
                case KeyType.TEST:
                    SetupKey(dotEnv.get("EASYPOST_TEST_KEY"));
                    break;
                case KeyType.PRODUCTION:
                    SetupKey(dotEnv.get("EASYPOST_PROD_KEY"));
                    break;
            }
        }

        public class Addresses : Mapper
        {
            public enum ADDRESS_RELATIONSHIP
            {
                SAME_STATE,
                DIFFERENT_STATE,
                SAME_COUNTRY,
                DIFFERENT_COUNTRY
            }

            public static Dictionary<string, object> GetMap()
            {
                var addressFile = Constants.Addresses.GetRandomAddressFile();
                var maps = JSONReader.GetRandomMapsFromJsonFile(addressFile, 1, true);
                return maps[0];
            }

            public static Dictionary<string, object> GetMap(Constants.Addresses.Country.COUNTRY country)
            {
                var addressFile = Constants.Addresses.GetAddressFile(country);
                var maps = JSONReader.GetRandomMapsFromJsonFile(addressFile, 1, true);
                return maps[0];
            }

            public static Dictionary<string, object> GetMap(Constants.Addresses.State.STATE state)
            {
                var addressFile = Constants.Addresses.GetAddressFile(state);
                var maps = JSONReader.GetRandomMapsFromJsonFile(addressFile, 1, true);
                return maps[0];
            }

            public static Address Get()
            {
                try
                {
                    var map = GetMap();
                    return Address.Create(map);
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static Address Get(Constants.Addresses.Country.COUNTRY country)
            {
                try
                {
                    var map = GetMap(country);
                    return Address.Create(map);
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static Address Get(Constants.Addresses.State.STATE state)
            {
                try
                {
                    var map = GetMap(state);
                    return Address.Create(map);
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static List<Dictionary<string, object>> GetMapsSameState(int amount)
            {
                var state = Constants.Addresses.State.GetRandom();
                var addressFile = Constants.Addresses.GetAddressFile(state);
                return JSONReader.GetRandomMapsFromJsonFile(addressFile, amount, true);
            }

            public static List<Address> GetSameState(int amount)
            {
                try
                {
                    var maps = GetMapsSameState(amount);
                    var addresses = new List<Address>();
                    foreach (var map in maps)
                    {
                        addresses.Add(Address.Create(map));
                    }

                    return addresses;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static List<Dictionary<string, object>> GetMapsDifferentStates(int amount)
            {
                if (amount > Constants.Addresses.State.Amount)
                {
                    throw new Exception($"Amount cannot be greater than {Constants.Addresses.State.Amount}");
                }

                var maps = new List<Dictionary<string, object>>();
                var states = Random.RandomItemsFromList(Constants.Addresses.State.Values, amount, false);
                foreach (var state in states)
                {
                    maps.Add(GetMap((Constants.Addresses.State.STATE)state));
                }

                return maps;
            }

            public static List<Address> GetDifferentStates(int amount)
            {
                try
                {
                    var maps = GetMapsDifferentStates(amount);
                    var addresses = new List<Address>();
                    foreach (Dictionary<string, object> map in maps)
                    {
                        addresses.Add(Address.Create(map));
                    }

                    return addresses;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static List<Dictionary<string, object>> GetMapsSameCountry(int amount)
            {
                var country = Constants.Addresses.Country.GetRandom();
                var addressFile = Constants.Addresses.GetAddressFile(country);
                return JSONReader.GetRandomMapsFromJsonFile(addressFile, amount, true);
            }

            public static List<Address> GetSameCountry(int amount)
            {
                try
                {
                    var maps = GetMapsSameCountry(amount);
                    var addresses = new List<Address>();
                    foreach (var map in maps)
                    {
                        addresses.Add(Address.Create(map));
                    }

                    return addresses;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static List<Dictionary<string, object>> GetMapsDifferentCountries(int amount)
            {
                if (amount > Constants.Addresses.Country.Amount)
                {
                    throw new Exception($"Amount cannot be greater than {Constants.Addresses.Country.Amount}");
                }

                var maps = new List<Dictionary<string, object>>();
                var countries = Random.RandomItemsFromList(Constants.Addresses.Country.Values, amount, false);
                foreach (var country in countries)
                {
                    maps.Add(GetMap((Constants.Addresses.Country.COUNTRY)country));
                }

                return maps;
            }

            public static List<Address> GetDifferentCountries(int amount)
            {
                try
                {
                    var maps = GetMapsDifferentCountries(amount);
                    var addresses = new List<Address>();
                    foreach (Dictionary<string, object> map in maps)
                    {
                        addresses.Add(Address.Create(map));
                    }

                    return addresses;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static List<Dictionary<string, object>> GetMaps(ADDRESS_RELATIONSHIP relationship, int amount)
            {
                switch (relationship)
                {
                    case ADDRESS_RELATIONSHIP.SAME_STATE:
                        return GetMapsSameState(amount);
                    case ADDRESS_RELATIONSHIP.DIFFERENT_STATE:
                        return GetMapsDifferentStates(amount);
                    case ADDRESS_RELATIONSHIP.SAME_COUNTRY:
                        return GetMapsSameCountry(amount);
                    case ADDRESS_RELATIONSHIP.DIFFERENT_COUNTRY:
                        return GetMapsDifferentCountries(amount);
                    default:
                        return null;
                }
            }

            public static List<Address> Get(ADDRESS_RELATIONSHIP relationship, int amount)
            {
                switch (relationship)
                {
                    case ADDRESS_RELATIONSHIP.SAME_STATE:
                        return GetSameState(amount);
                    case ADDRESS_RELATIONSHIP.DIFFERENT_STATE:
                        return GetDifferentStates(amount);
                    case ADDRESS_RELATIONSHIP.SAME_COUNTRY:
                        return GetSameCountry(amount);
                    case ADDRESS_RELATIONSHIP.DIFFERENT_COUNTRY:
                        return GetDifferentCountries(amount);
                    default:
                        return null;
                }
            }
        }

        public class Parcels : Mapper
        {
            public static Dictionary<string, object> GetMap()
            {
                var map = new Dictionary<string, object>();
                map.Add("weight", Random.RandomIntInRange(0, 100));
                map.Add("height", Random.RandomIntInRange(0, 100));
                map.Add("width", Random.RandomIntInRange(0, 100));
                map.Add("length", Random.RandomIntInRange(0, 100));

                return map;
            }

            public static Parcel Get()
            {
                try
                {
                    var map = GetMap();
                    return Parcel.Create(map);
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static Parcel Retrieve(string id)
            {
                try
                {
                    return Parcel.Retrieve(id);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public class Insurance
        {
            public static Dictionary<string, object> GetMap(float amount)
            {
                var map = new Dictionary<string, object>();
                map.Add("amount", amount);
                return map;
            }

            public static Dictionary<string, object> GetMap()
            {
                return GetMap(Random.RandomFloatInRange(1, 100));
            }

            public static Shipment Insure(Shipment shipment)
            {
                return Shipments.AddInsurance(shipment);
            }

            public static Shipment Insure(Shipment shipment, float amount)
            {
                return Shipments.AddInsurance(shipment, amount);
            }
        }

        public class Shipments : Mapper
        {
            public static Dictionary<string, object> GetMap()
            {
                try
                {
                    var addressMaps = Addresses.GetMapsDifferentStates(2);
                    var parcelMap = Parcels.GetMap();

                    var shipmentMap = new Dictionary<string, object>();
                    shipmentMap.Add("to_address", addressMaps[0]);
                    shipmentMap.Add("from_address", addressMaps[1]);
                    shipmentMap.Add("parcel", parcelMap);
                    return parcelMap;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static Dictionary<string, object> GetMap(Dictionary<string, object> toAddressMap,
                Dictionary<string, object> fromAddressMap, Dictionary<string, object> parcelMap)
            {
                try
                {
                    var shipmentMap = new Dictionary<string, object>();
                    shipmentMap.Add("to_address", toAddressMap);
                    shipmentMap.Add("from_address", fromAddressMap);
                    shipmentMap.Add("parcel", parcelMap);
                    return shipmentMap;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static Dictionary<string, object> GetReturnMap()
            {
                try
                {
                    var map = GetMap();
                    map.Add("is_return", true);
                    return map;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static Dictionary<string, object> GetReturnMap(Dictionary<string, object> toAddressMap,
                Dictionary<string, object> fromAddressMap, Dictionary<string, object> parcelMap)
            {
                try
                {
                    var map = GetMap(toAddressMap, fromAddressMap, parcelMap);
                    map.Add("is_return", true);
                    return map;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static Shipment Get()
            {
                try
                {
                    var map = GetMap();
                    return Create(map);
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static Shipment GetReturn()
            {
                try
                {
                    var map = GetReturnMap();
                    return Create(map);
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static Shipment Create(Dictionary<string, object> shipmentMap)
            {
                try
                {
                    return Shipment.Create(shipmentMap);
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static Shipment AddInsurance(Shipment shipment)
            {
                try
                {
                    var insuranceMap = Insurance.GetMap();
                    shipment.Insure((double)insuranceMap["amount"]);
                    return shipment;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static Shipment AddInsurance(Shipment shipment, float amount)
            {
                try
                {
                    shipment.Insure(amount);
                    return shipment;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static Shipment Refund(Shipment shipment)
            {
                try
                {
                    shipment.Refund();
                    return shipment;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static Dictionary<string, object> MarkForReturn(Dictionary<string, object> shipmentMap)
            {
                try
                {
                    shipmentMap.Add("is_return", true);
                    return shipmentMap;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public class Options
        {
            public static Dictionary<string, object> GetMap()
            {
                var maps = JSONReader.GetRandomMapsFromJsonFile(Constants.OPTIONS_JSON, 1, true);
                return maps[0];
            }
        }

        public class Rates : Mapper
        {
            public static List<Rate> Get()
            {
                try
                {
                    var shipment = Shipments.Get();
                    return Get(shipment);
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static List<Rate> Get(Dictionary<string, object> shipmentMap)
            {
                try
                {
                    var shipment = Shipment.Create(shipmentMap);
                    return Get(shipment);
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static List<Rate> Get(Shipment shipment)
            {
                try
                {
                    shipment.GetRates();
                    return shipment.rates;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public class Smartrates : Mapper
        {
            public static List<Smartrate> Get()
            {
                try
                {
                    var shipment = Shipments.Get();
                    return Get(shipment);
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static List<Smartrate> Get(Dictionary<string, object> shipmentMap)
            {
                try
                {
                    var shipment = Shipment.Create(shipmentMap);
                    return Get(shipment);
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static List<Smartrate> Get(Shipment shipment)
            {
                try
                {
                    return shipment.GetSmartrates();
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public class TaxIdentifiers : Mapper
        {
            public static Dictionary<string, object> GetMap()
            {
                try
                {
                    var maps = JSONReader.GetRandomMapsFromJsonFile(Constants.TRACKERS_JSON, 1, true);
                    return maps[0];
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static Tracker Get()
            {
                try
                {
                    var map = GetMap();
                    return Tracker.Create(map["carrier"].ToString(), map["tracking_code"].ToString());
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public class Batch
        {
        }

        public class CustomsInfos : Mapper
        {
            public static Dictionary<string, object> GetMap(int itemsAmount, bool allowDuplicateItems)
            {
                var maps = JSONReader.GetRandomMapsFromJsonFile(Constants.CUSTOMS_INFO_JSON, 1, true);
                var map = maps[0];
                map.Add("customs_items", CustomsItems.GetRandomCustomsItemMaps(itemsAmount, allowDuplicateItems));
                return map;
            }

            public static CustomsInfo Get(int amount, bool allowDuplicateItems)
            {
                try
                {
                    var map = GetMap(amount, allowDuplicateItems);
                    return CustomsInfo.Create(map);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public class CustomsItems : Mapper
        {
            public static List<Dictionary<string, object>> GetRandomCustomsItemMaps(int amount, bool allowDuplicates)
            {
                return JSONReader.GetRandomMapsFromJsonFile(Constants.CUSTOMS_ITEMS_JSON, amount, allowDuplicates);
            }

            public static List<CustomsItem> Get(int amount, bool allowDuplicates)
            {
                try
                {
                    var maps = GetRandomCustomsItemMaps(amount, allowDuplicates);
                    var customsItems = new List<CustomsItem>();
                    foreach (Dictionary<string, object> map in maps)
                    {
                        customsItems.Add(CustomsItem.Create(map));
                    }

                    return customsItems;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public class Events
        {
        }

        public class Fees : Mapper
        {
            public static List<Fee> Get()
            {
                try
                {
                    var shipment = Shipments.Get();
                    return Get(shipment);
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static List<Fee> Get(Shipment shipment)
            {
                try
                {
                    return shipment.fees;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static List<Fee> Get(Dictionary<string, object> shipmentMap)
            {
                try
                {
                    var shipment = Shipments.Create(shipmentMap);
                    return Get(shipment);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public class Orders
        {
        }

        public class Pickups
        {
            public static Dictionary<string, object> GetMap()
            {
                try
                {
                    var maps = JSONReader.GetRandomMapsFromJsonFile(Constants.PICKUPS_JSON, 1, true);
                    var map = maps[0];
                    var toAddressMap = Addresses.GetMap();
                    var fromAddressMap = Addresses.GetMap();
                    map.Add("address", toAddressMap);

                    var parcelMap = Parcels.GetMap();
                    var shipmentMap = Shipments.GetMap(toAddressMap, fromAddressMap, parcelMap);
                    map.Add("shipment", shipmentMap);

                    var dates = Dates.GetFutureDates(2);
                    map.Add("min_datetime", dates[0].ToString());
                    map.Add("max_datetime", dates[1].ToString());

                    return map;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public class Reports : Mapper
        {
            public static Dictionary<string, object> GetMap()
            {
                try
                {
                    var dateMap = new Dictionary<string, object>();
                    var dates = Dates.GetPastDates(2);
                    dateMap.Add("start_date", dates[1].ToString());
                    dateMap.Add("end_date", dates[0].ToString());

                    var map = new Dictionary<string, object>();
                    map.Add("shipment", dateMap);
                    return map;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static Report Get()
            {
                try
                {
                    var map = GetMap();
                    return Report.Create("shipment", (Dictionary<string, object>)map["shipment"]);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public class ScanForms
        {
        }

        public class Webhooks : Mapper
        {
            public static Dictionary<string, object> GetMap()
            {
                try
                {
                    var map = new Dictionary<string, object>();
                    map.Add("url", "http://www.google.com");
                    return map;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static Webhook Get()
            {
                try
                {
                    var map = GetMap();
                    return Webhook.Create(map);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public class Users
        {
        }

        public class Carriers
        {
            public static List<string> Get(int amount)
            {
                var carrierStrings = new List<string>();
                var carriers = JSONReader.GetRandomItemsFromJsonFile(Constants.CARRIERS_JSON, amount, false);
                foreach (var carrier in carriers)
                {
                    carrierStrings.Add(carrier.ToString());
                }

                return carrierStrings;
            }

            public static string Get()
            {
                var carriers = Get(1);
                return carriers[0];
            }
        }

        public class Labels
        {
            public static Dictionary<string, object> GetRandomLabelOptions()
            {
                var maps = JSONReader.GetRandomMapsFromJsonFile(Constants.LABEL_OPTIONS_JSON, 1, true);
                return maps[0];
            }
        }

        public class PostageLabels
        {
            public static PostageLabel Get()
            {
                try
                {
                    var shipment = Shipments.Get();
                    return Get(shipment);
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static PostageLabel Get(Dictionary<string, object> shipmentMap)
            {
                try
                {
                    var shipment = Shipment.Create(shipmentMap);
                    return Get(shipment);
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            public static PostageLabel Get(Shipment shipment)
            {
                try
                {
                    shipment.GenerateLabel("pdf");
                    return shipment.postage_label;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
    }
}