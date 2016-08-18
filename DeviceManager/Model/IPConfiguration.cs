using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeviceManager.Model
{
    public class IPConfiguration
    {
        public string Description { get; internal set; }
        public string HardwareAddress { get; internal set; }
        public int Index { get; internal set; }
        public string Name { get; internal set; }
        public string Type { get; internal set; }

        public DHCP DHCP { get; internal set; }
        public Address Address { get; internal set; }
        public Gateways[] Gateways { get; internal set; }
        public IPAddress[] IpAddresses { get; internal set; }
    }
    public class DHCP
    {
        public string LeaseExpires { get; internal set; }
        public string LeaseObtained { get; internal set; }

    }
    public class Address
    {
        public string IpAddress { get; internal set; }
        public string Mask { get; internal set; }
    }

    public class Gateways

    {
        public string IpAddress { get; internal set; }
        public string Mask { get; internal set; }
    }

    public class IPAddress
    {
        public string IpAddress { get; internal set; }
        public string Mask { get; internal set; }
    }


}
