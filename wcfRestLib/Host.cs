using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.ServiceModel;
using Indanet.neXus.Debugging;

namespace wcfRestLib
{
    public class Host
    {
        WardTrace TRACE = Indanet.neXus.Application.TRACE;
        VideoProviderRestApi m_restApi;
        WebServiceHost m_Host;
        Uri m_baseUri;

        public Host(string baseUri)
        {
            m_baseUri = new Uri(baseUri);
            m_restApi = new VideoProviderRestApi(new VideoProvider(), m_baseUri, TRACE);

            TRACE.Info("Host created!!!");
        }

        public void Start()
        {
            m_Host = new WebServiceHost(m_restApi, m_baseUri);
            ServiceEndpoint ep = m_Host.AddServiceEndpoint(m_restApi.GetType(), new WebHttpBinding(), "");
            ServiceDebugBehavior stp = m_Host.Description.Behaviors.Find<ServiceDebugBehavior>();
            stp.HttpHelpPageEnabled = false;
            m_Host.Open();

            TRACE.Info("Host started at {0}", m_baseUri);
        }
    }
}
