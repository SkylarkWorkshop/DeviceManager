using DeviceManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager
{
    public class XboxDevice : IDevice
    {
        public XboxDevice(string addr)
        {
            Address = addr;
        }
        public string Address
        {
            get;
        }

        public DeviceType DeviceType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsAuthed
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsConnected
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReady
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Task<IList<AppxPackage>> GetAppsInfoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Process>> GetProcessesInfoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SystemPerf> GetSystemPerfAsync()
        {
            throw new NotImplementedException();
        }

        public Task LaunchAppAsync(string appid, string packageName)
        {
            throw new NotImplementedException();
        }

        public void Reboot()
        {
            throw new NotImplementedException();
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        public Task UninstallAppAsync(string packageName)
        {
            throw new NotImplementedException();
        }
    }
}
