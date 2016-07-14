using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceManager.Model;

namespace DeviceManager.Manager
{
    class AppsManager
    {
        HttpClient client;
        public string Address {get; set;}
        public bool IsConnected {get; set;}
    }
}