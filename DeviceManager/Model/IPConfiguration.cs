using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeviceManager.Model
{
    public class IPConfiguration
    {
        public string Description { get; set; }
        public string HardwareAddress { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public DHCP DHCP { get; set; }
        public Address Address { get; set; }
        public Gateways[] Gateways { get; set; }
        public IPAddress[] IpAddresses { get; set; }
    }
    public class DHCP
    {
        public string LeaseExpires { get; set; }
        public string LeaseObtained { get; set; }

    }
    public class Address
    {
        public string IpAddress { get; set; }
        public string Mask { get; set; }
    }

    public class Gateways

    {
        public string IpAddress { get; set; }
        public string Mask { get; set; }
    }

    public class IPAddress
    {
        public string IpAddress { get; set; }
        public string Mask { get; set; }
    }


}
