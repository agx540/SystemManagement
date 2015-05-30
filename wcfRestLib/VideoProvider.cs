using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wcfRestLib
{
    public class VideoProvider
    {
        public string SystemName { get; protected set; }
        public VideoProvider(string name)
        {
            SystemName = name;
        }
    }
}
