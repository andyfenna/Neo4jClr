using System;
using Neo4jClient;

namespace Neo4jClr
{
    public static class Client
    {
        public static string Url { get; set; }

        public static IGraphClient Instance()
        {
            if (Url != null)
            {
                var client = new GraphClient(new Uri(Url));
                Console.Write("Connection successfull.");
                client.Connect();
                return client;
            }
            return null;
        }
    }
}