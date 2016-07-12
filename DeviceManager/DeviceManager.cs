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
        public static IDevice Connect(DeviceType deviceType,ConnectionType connType,string addr)
        {
            switch(deviceType)
            {
                case DeviceType.Mobile:
                    switch(connType)
                    {
                        case ConnectionType.Local:return MobileDeviceManager.ConnectLocal();
                        case ConnectionType.Remote:return MobileDeviceManager.ConnectRemote(addr);
                    }
                    break;
                case DeviceType.HoloLens:
                    switch(connType)
                    {
                        case ConnectionType.Local:return HoloLensDeviceManager.ConnectLocal();
                        case ConnectionType.Remote:return HoloLensDeviceManager.ConnectRemote(addr);
                    }
                    break;
                case DeviceType.Xbox:
                    switch(connType)
                    {
                        case ConnectionType.Local:return XboxDeviceManager.ConnectLocal();
                        case ConnectionType.Remote:return XboxDeviceManager.ConnectRemote(addr);
                    }
                    break;
                case DeviceType.PC:
                    throw new NotImplementedException("PC platform is not supported.");
            }
            return null;
        }
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
        Xbox
    }
    public enum ConnectionType
    {
        Local,
        Remote
    }
}
