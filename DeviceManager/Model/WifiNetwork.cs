using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeviceManager.Model
{
    public class WifiNetwork
    {
        public bool AlreadyConnected { get; set; }
        public string AuthenticationAlgorithm { get; set; }
        public int Channel { get; set; }
        public int Index { get; set; }
        public string CipherAlgorithm { get; set; }
        public int Connectable { get; set; }
        public string InfrastructureType { get; set; }
        public bool ProfileAvailable { get; set; }
        public string ProfileName { get; set; }
        public string SSID { get; set; }
        public string SecurityEnabled { get; set; }
        public string SignalQuality { get; set; }
        public int[] BSSID { get; set; }
        public string[] PhysicalTypes { get; set; }

    }
}
