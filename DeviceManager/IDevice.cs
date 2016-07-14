using DeviceManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager
{
    public interface IDevice
    {
        DeviceType DeviceType { get; }
        string Address { get; }
        bool IsReady { get; }
        bool IsAuthed { get; }
        bool IsConnected { get; }
        void Shutdown();
        void Reboot();
        Task<IList<Process>> GetProcessesInfoAsync();
        Task<IList<AppxPackage>> GetAppsInfoAsync();
    }
}
