using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeviceManager.Model
{
    public class HardwareDevice
    {
        public string Class { get; internal set; }
        public string Description { get; internal set; }
        public string FriendlyName { get; internal set; }
        public string ID { get; internal set; }
        public string Manufacturer { get; internal set; }
        public string ParentID { get; internal set; }
        public int ProblemCode { get; internal set; }
        public int StatusCode { get; internal set; }

    }
}
