using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager
{
    public class HoloLensDeviceManager
    {
        /// <summary>
        /// Connect to local device (127.0.0.1).
        /// </summary>
        /// <returns></returns>
        public static HoloLensDevice ConnectLocal()
        {
            return new HoloLensDevice("127.0.0.1");
        }
        /// <summary>
        /// Connect to a remote device
        /// <para>
        /// Capability "Proximity" required
        /// </para>
        /// </summary>
        /// <returns></returns>
        public static HoloLensDevice ConnectRemote(string addr)
        {
            return new HoloLensDevice(addr);
        }
    }
}
