using Nest;

namespace LoggerUsingElasticSearch
{
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
}
