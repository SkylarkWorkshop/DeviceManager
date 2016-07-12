using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager
{
    public class HoloLensDevice : IDevice
    {
        public HoloLensDevice(string addr)
        {
            Address = addr;
        }
        public string Address
        {
            get;
        }

        public DeviceType DeviceType
        {
           get { return DeviceType.HoloLens; }
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

        public Task<IList<Process>> GetProcessesInfo()
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
    }
}
