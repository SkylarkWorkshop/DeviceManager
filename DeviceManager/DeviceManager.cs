using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager
{
    public class DeviceManager
    {
        
        public class DeviceConnectionException : Exception
        {
            public HttpStatusCode Code { get; private set; }
            public DeviceConnectionException(string message, HttpStatusCode code) : base(message)
            {
                Code = code;
            }
            public DeviceConnectionException(string message) : base(message)
            {

            }
        }
    }
    public enum DeviceType
    {
        Mobile,
        PC,
        HoloLens,
        Xbox,
        IoT
    }
    public enum ConnectionType
    {
        Local,
        Remote
    }
}
