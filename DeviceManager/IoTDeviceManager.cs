using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager
{
    public class IoTDeviceManager
    {
        /// <summary>
        /// Connect to local device (127.0.0.1).
        /// </summary>
        /// <returns></returns>
        public static IoTDevice ConnectLocal()
        {
            return new IoTDevice("127.0.0.1:8080");
        }
        /// <summary>
        /// Connect to a remote device
        /// <para>
        /// Capability "Proximity" required. Port '8080' should be added to the end of the address
        /// </para>
        /// </summary>
        /// <returns></returns>
        public static IoTDevice ConnectRemote(string addr)
        {
            return new IoTDevice(addr);
        }
    }
}
