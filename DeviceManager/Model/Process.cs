using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.Model
{
    public class Process
    {
        public double CPUUsage { get; internal set; }
        public string ImageName { get; internal set; }
        public double PageFileUsage { get; internal set; }
        public double PrivateWorkingSet { get; internal set; }
        public double ProcessId { get; internal set;}
        public double SessionId { get; internal set; }
        public double TotalCommit { get; internal set; }
        public string UserName { get; internal set; }
        public double VirtualSize { get; internal set; }
        public double WorkingSetSize { get; internal set; }
        public object Version { get; internal set; }
        public string PackageFullName { get; internal set; }
        public string Publisher { get; internal set; }
    }
}
