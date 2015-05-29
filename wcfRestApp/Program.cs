using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wcfRestLib;
using Indanet.neXus.Debugging;

namespace wcfRestApp
{
    class Program
    {
        static WardTrace STRACE = Indanet.neXus.Application.TRACE;

        static void Main(string[] args)
        {
            STRACE.Info("AppStarted!");

            Host host;
            List<Host> hosts = new List<Host>();
            
            for(int i = 9001; i < 9011; i++)
            {
                string uri = string.Format("http://localHost:{0}/Provider", i);
                host = new Host(uri);
                host.Start();

                hosts.Add(host);
            }
            
            Console.ReadLine();
        }
    }
}
