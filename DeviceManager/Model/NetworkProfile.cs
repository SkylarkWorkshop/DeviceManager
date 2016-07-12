using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.Model
{
    public class NetworkProfile
    {      
        public bool IsAlreadyConnected
        {
            get;
            set;
        }
        
        public string SSID
        {
            get;
            set;
        }
       
        public bool SecurityEnabled
        {
            get;
            set;
        }
       
        public int SignalQuality
        {
            get;
            set;
        }
        
        public string AuthenticationAlgorithm
        {
            get;
            set;
        }
        
        public string CipherAlgorithm
        {
            get;
            set;
        }
        
        public bool IsConnectable
        {
            get;
            set;
        }
       
        public string InfrastructureType
        {
            get;
            set;
        }
       
        public bool IsProfileAvailable
        {
            get;
            set;
        }
       
        public string ProfileName
        {
            get;
            set;
        }
        
        public string[] PhysicalTypes
        {
            get;
            set;
        }
    }
}
