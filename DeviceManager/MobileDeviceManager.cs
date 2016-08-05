using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace DeviceManager
{
    public class MobileDeviceManager
    {
        /// <summary>
        /// Connect to local device.
        /// </summary>
        /// <returns></returns>
        public static MobileDevice ConnectLocal()
        {
            return new MobileDevice("127.0.0.1");
        }
        /// <summary>
        /// Connect to a remote device
        /// <para>
        /// Capability "Proximity" required
        /// </para>
        /// </summary>
        /// <returns></returns>
        public static MobileDevice ConnectRemote(string addr)
        {
            return new MobileDevice(addr);
        }
    }
}
