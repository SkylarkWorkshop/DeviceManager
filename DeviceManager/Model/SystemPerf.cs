using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.Model
{
    public class SystemPerf
    {
        public int AvailablePages { get; internal set; }
        public int CommitLimit { get; internal set; }
        public int CommittedPages { get; internal set; }
        public int CpuLoad { get; internal set; }
        public int IOOtherSpeed { get; internal set; }
        public int IOReadSpeed { get; internal set; }
        public int IOWriteSpeed { get; internal set; }
        public int NonPagedPoolPages { get; internal set; }
        public int PageSize { get; internal set; }
        public int PagedPoolPages { get; internal set; }
        public int TotalPages { get; internal set; }
        public GpuData GPUData { get; internal set; }
        public NetData NetworkingData { get; internal set; }
        public int TotalInstalledInKb { get; internal set; }

        public class GpuData
        {
            public class GpuAdapter
            {
                public int DedicatedMemory { get; internal set; }
                public int DedicatedMemoryUsed { get; internal set; }
                public string Description { get; internal set; }
                public int SystemMemory { get; internal set; }
                public int SystemMemoryUsed { get; internal set; }
                public IEnumerable<float> EnginesUtilization { get; internal set; }
            }
            public IEnumerable<GpuAdapter> AvailableGPUAdapters { get; internal set; }
        }
        public class NetData
        {
            public int NetworkInBytes { get; internal set; }
            public int NetworkOutBytes { get; internal set; }
        }
    }
}
