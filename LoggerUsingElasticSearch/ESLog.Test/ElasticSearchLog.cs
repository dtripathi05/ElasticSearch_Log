using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoggerUsingElasticSearch;
using System.Net;

namespace ESLog.Test
{
    [TestClass]
    public class ElasticSearch
    {
        private Log log1;
        private Log log2;
        private Post post;
        public ElasticSearch()
        {

           // ConnectClient con = new ConnectClient();
            log1 = new Log("Successful",DateTime.Now,DateTime.Now.Add(new TimeSpan(0,0,1,0)), Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString(), Guid.NewGuid().ToString());
            log2 = new Log("Sucessful",DateTime.Now,DateTime.Now.Add(new TimeSpan(0, 0, 1, 0)), Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString(), Guid.NewGuid().ToString());
            post = new Post("logger", "log_entry");
        }
        [TestMethod]
        public void Creating_Log_ES()
        {
            var result = post.AddNewIndex(log1);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Searching_Log_ES()
        {
            var result2 = post.AddNewIndex(log2);
            Search search = new Search();
            var val = search.GetResult("logger", "log_entry");
            Assert.AreEqual(2, val.Count);
        }

    }
}
