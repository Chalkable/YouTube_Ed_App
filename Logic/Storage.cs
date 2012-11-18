using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Youtube.Logic
{
    public class YoutubeEntity : TableEntity
    {
        public const string PART_KEY = "Youtube";
        public YoutubeEntity(string key)
        {
            PartitionKey = PART_KEY;
            RowKey = key;
        }

        public YoutubeEntity() { }

        public string Value { get; set; }
    }

    public class Storage
    {
        private const string CONNECTION_STRING_NAME = "Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString";
        private CloudStorageAccount storageAccount;
        
        private CloudStorageAccount GetStorageAccount()
        {
            if (storageAccount == null)
            {
                string connectionString = RoleEnvironment.GetConfigurationSettingValue(CONNECTION_STRING_NAME);
                storageAccount = CloudStorageAccount.Parse(connectionString);
            }
            return storageAccount;
        }

        private CloudTable GetTable()
        {
            var tableClient = GetStorageAccount().CreateCloudTableClient();
            var table = tableClient.GetTableReference("youtube");
            table.CreateIfNotExists();
            return table;
        }

        public string Get(string key)
        {
            var t = GetTable();
            TableOperation retrieveOperation = TableOperation.Retrieve<YoutubeEntity>(YoutubeEntity.PART_KEY, key);
            TableResult retrievedResult = t.Execute(retrieveOperation);
            if (retrievedResult.Result != null)
                return ((YoutubeEntity)retrievedResult.Result).Value;
            return null;
        }

        public int GetInt(string key)
        {
            return int.Parse(Get(key));
        }

        public void Set(string key, object value)
        {
            var entity = new YoutubeEntity(key);
            entity.Value = value.ToString();
            TableOperation insertOperation = TableOperation.InsertOrReplace(entity);
            GetTable().Execute(insertOperation);
        }
    }

    /*public class Storage
    {
        private static Dictionary<string, string> storage = new Dictionary<string, string>();


        public string Get(string key)
        {
            return storage[key];
        }

        public int GetInt(string key)
        {
            return int.Parse(Get(key));
        }

        public void Set(string key, object value)
        {
            if (!storage.ContainsKey(key))
                storage.Add(key, value.ToString());
            else
                storage[key] = value.ToString();
        }
    }*/
}