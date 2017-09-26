using System;
using Nest;
using System.Collections.Generic;
using System.Linq;

namespace LoggerUsingElasticSearch
{
    public class Log
    {
        public string Status { get; private set; }
        public DateTime RequestTime { get; private set; }
        public DateTime ResponseTime { get; private set; }
        public string IpAddress { get; private set; }
        public string SessionId { get; private set; }
        public Log(string status, DateTime requestTime, DateTime responseTime, string ipAddress, string sessionId)
        {
            Status = status;
            RequestTime = requestTime;
            ResponseTime = responseTime;
            IpAddress = ipAddress;
            SessionId = sessionId;
        }

    }
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
    public class Post
    {
        private string _index { get; set; }
        private string _type { get; set; }
        public Post(string index, string type)
        {
            _index = index;
            _type = type;
        }
        public IIndexResponse AddNewIndex(Log log)
        {
            var response = ConnectClient.client.Index(log, i => i.Index(_index).Type(_type).Id(log.SessionId));
            return response;
        }
    }
    public class Search
    {
        public List<Log> GetResult(string index, string type)
        {

            if (ConnectClient.client.IndexExists("logger").Exists)
            {
                var response = ConnectClient.client.Search<Log>(i => i.Index(index).Type(type));
                return response.Documents.ToList();
            }
            return null;
        }

        public List<Log> GetResult(string condition, string index, string type)
        {
            if (ConnectClient.client.IndexExists("logger").Exists)
            {
                var query = condition;

                return ConnectClient.client.SearchAsync<Log>(s => s
                .Index(index)
                .Type(type)
                .From(0)
                .Take(10)
                .Query(qry => qry
                    .Bool(b => b
                        .Must(m => m
                            .QueryString(qs => qs
                                .DefaultField("_all")
                                .Query(query)))))).Result.Documents.ToList();
            }
            return null;
        }
    }
}
