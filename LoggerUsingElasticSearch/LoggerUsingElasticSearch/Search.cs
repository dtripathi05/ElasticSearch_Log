using System.Collections.Generic;
using System.Linq;

namespace LoggerUsingElasticSearch
{
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
