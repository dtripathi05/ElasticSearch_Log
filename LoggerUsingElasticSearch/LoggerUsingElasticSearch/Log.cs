using System;

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
}
