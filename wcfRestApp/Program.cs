using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wcfRestLib;

namespace wcfRestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("App started");

            Host host;
            List<Host> hosts = new List<Host>();

            int camCount = 10;
            for(int i = 9001; i < 9011; i++)
            {
                string uri = string.Format("http://localHost:{0}/Provider", i);
                VideoProviderRestApi videoProvider = new VideoProviderRestApi(new VideoProvider("ProviderName" + i), new Uri(uri), camCount);
                host = new Host(uri, videoProvider);
                host.Start();
                
                hosts.Add(host);

                camCount++;
            }
            
            Console.ReadLine();
        }
    }
}
