using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.Model
{
    public class SystemPerf
    {
        public double AvailablePages { get; set; }
        public double CommitLimit { get; set; }
        public double CommittedPages { get; set; }
        public double CpuLoad { get; set; }
        public double IOOtherSpeed { get; set; }
        public double IOReadSpeed { get; set; }
        public double IOWriteSpeed { get; set; }
        public double NonPagedPoolPages { get; set; }
        public double PageSize { get; set; }
        public double PagedPoolPages { get; set; }
        public double TotalPages { get; set; }
        public GpuData GPUData { get; set; }
        public NetData NetworkingData { get; set; }
        public class GpuData
        {
            public class GpuAdapter
            {
                public double DedicatedMemory { get; set; }
                public double DedicatedMemoryUsed { get; set; }
                public string Description { get; set; }
                public double SystemMemory { get; set; }
                public double SystemMemoryUsed { get; set; }
            }
            public GpuAdapter[] AvailableGPUAdapters { get; set; }
        }
        public class NetData
        {
            public double NetworkInBytes { get; set; }
            public double NetworkOutBytes { get; set; }
        }
    }
}
