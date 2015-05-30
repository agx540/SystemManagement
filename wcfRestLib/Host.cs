using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.ServiceModel;

namespace wcfRestLib
{
    public class Host
    {
        VideoProviderRestApi m_restApi;
        WebServiceHost m_Host;
        Uri m_baseUri;

        public Host(string baseUri, VideoProviderRestApi videoProvider)
        {
            m_baseUri = new Uri(baseUri);
            m_restApi = videoProvider;

            Console.WriteLine("Host created!");
        }

        public void Start()
        {
            try
            {
                m_Host = new WebServiceHost(m_restApi, m_baseUri);
                ServiceEndpoint ep = m_Host.AddServiceEndpoint(m_restApi.GetType(), new WebHttpBinding(), "");
                ServiceDebugBehavior stp = m_Host.Description.Behaviors.Find<ServiceDebugBehavior>();
                stp.HttpHelpPageEnabled = false;
                m_Host.Open();

                Console.WriteLine(string.Format("Host started at {0}", m_baseUri));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Starting host failed for {0}\n{1}", m_baseUri, ex));
            }
        }
    }
}
