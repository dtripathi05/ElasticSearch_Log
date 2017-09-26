using System;
using Nest;

namespace LoggerUsingElasticSearch
{
    public class ConnectClient
    {
        public static Uri node;
        public static ElasticClient client = null;
        public static ConnectionSettings settings;
        public ConnectClient()
        {
            node = new Uri("http://172.16.14.121:9200/");
            var indexsettings = new IndexSettings();
            indexsettings.NumberOfReplicas = 1;
            indexsettings.NumberOfShards = 1;
            settings = new ConnectionSettings(node);
            var rese = settings.DefaultIndex("log");
            client = new ElasticClient(settings);
        }
    }
}
