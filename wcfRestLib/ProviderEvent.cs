using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace wcfRestLib
{
    [DataContract]
    public class ProviderEvent
    {
        [DataMember]
        public DateTime StartTimestampUtc { get; set; }

        [DataMember]
        public DateTime EndTimestampUtc { get; set; }

        [DataMember]
        public string EventSource { get; set; }

        [DataMember]
        public string EventType { get; set; }

        [DataMember]
        public string EventDescription { get; set; }

        public override string ToString()
        {
            StringBuilder buf = new StringBuilder(1024);

            buf.AppendFormat("StartTimestampUtc    = {0}", StartTimestampUtc.ToString("u"));
            buf.AppendFormat("EndTimestampUtc      = {0}", EndTimestampUtc.ToString("u"));
            buf.AppendFormat("EventSource          = {0}", String.IsNullOrEmpty(EventSource) ? "<empty" : EventSource);
            buf.AppendFormat("EventType            = {0}", String.IsNullOrEmpty(EventType) ? "<empty" : EventType);
            buf.AppendFormat("EventDescription     = {0}", String.IsNullOrEmpty(EventDescription) ? "<empty" : EventDescription);

            return buf.ToString();
        }

    }
}
