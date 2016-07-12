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
            return new IoTDevice("127.0.0.1");
        }
        /// <summary>
        /// Connect to a remote device
        /// <para>
        /// Capability "Proximity" required
        /// </para>
        /// </summary>
        /// <returns></returns>
        public static IoTDevice ConnectRemote(string addr)
        {
            return new IoTDevice(addr);
        }
    }
}
