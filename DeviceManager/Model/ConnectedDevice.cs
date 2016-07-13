using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeviceManager.Model
{
    public class ConnectedDevice
    {
        public string Class { get; set; }
        public string Description { get; set; }
        public string FriendlyName { get; set; }
        public string ID { get; set; }
        public string Manufacturer { get; set; }
        public string ParentID { get; set; }
        public string ProblemCode { get; set; }
        public string StatusCode { get; set; }

    }
}
