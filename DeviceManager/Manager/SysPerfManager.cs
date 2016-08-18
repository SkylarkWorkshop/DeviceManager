using DeviceManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Web.Http;
using static DeviceManager.DeviceManager;

namespace DeviceManager.Manager
{
    public class SysPerfManager
    {
        public static async Task<SystemPerf> GetSystemPerfAsync(HttpClient client,string addr)
        {
            var res = await client.GetAsync(new Uri("https://" + addr + "/api/resourcemanager/systemperf"));
            var responseText = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode == true)
            {
                JsonObject jobj = JsonObject.Parse(responseText);
                List<SystemPerf.GpuData.GpuAdapter> gpus = new List<SystemPerf.GpuData.GpuAdapter>();
                SystemPerf sysperf = new SystemPerf()
                {
                    AvailablePages = jobj.ContainsKey("AvailablePages") ? Convert.ToInt32(jobj["AvailablePages"].GetNumber()) : 0,
                    CommitLimit = jobj.ContainsKey("CommitLimit") ? Convert.ToInt32(jobj["CommitLimit"].GetNumber()) : 0,
                    CommittedPages = jobj.ContainsKey("CommittedPages") ? Convert.ToInt32(jobj["CommittedPages"].GetNumber()) : 0,
                    CpuLoad = jobj.ContainsKey("CpuLoad") ? Convert.ToInt32(jobj["CpuLoad"].GetNumber()) : 0,
                    IOOtherSpeed = jobj.ContainsKey("IOOtherSpeed") ? Convert.ToInt32(jobj["IOOtherSpeed"].GetNumber()) : 0,
                    IOReadSpeed = jobj.ContainsKey("IOReadSpeed") ? Convert.ToInt32(jobj["IOReadSpeed"].GetNumber()) : 0,
                    IOWriteSpeed = jobj.ContainsKey("IOWriteSpeed") ? Convert.ToInt32(jobj["IOWriteSpeed"].GetNumber()) : 0,
                    NonPagedPoolPages = jobj.ContainsKey("NonPagedPoolPages") ? Convert.ToInt32(jobj["NonPagedPoolPages"].GetNumber()) : 0,
                    PageSize = jobj.ContainsKey("PageSize") ? Convert.ToInt32(jobj["PageSize"].GetNumber()) : 0,
                    PagedPoolPages = jobj.ContainsKey("PagedPoolPages") ? Convert.ToInt32(jobj["PagedPoolPages"].GetNumber()) : 0,
                    TotalInstalledInKb = jobj.ContainsKey("TotalInstalledInKb") ? Convert.ToInt32(jobj["TotalInstalledInKb"].GetNumber()) : 0,
                    TotalPages = jobj.ContainsKey("TotalPages") ? Convert.ToInt32(jobj["TotalPages"].GetNumber()) : 0,
                    NetworkingData = jobj.ContainsKey("NetworkingData") ? new SystemPerf.NetData()
                    {                        
                        NetworkInBytes = jobj["NetworkingData"].GetObject().ContainsKey("NetworkInBytes") ?Convert.ToInt32(jobj["NetworkingData"].GetObject()["NetworkInBytes"].GetNumber()):0,
                        NetworkOutBytes = jobj["NetworkingData"].GetObject().ContainsKey("NetworkOutBytes") ? Convert.ToInt32(jobj["NetworkingData"].GetObject()["NetworkOutBytes"].GetNumber()):0
                    } : null

                };
                if(jobj.ContainsKey("GPUData"))
                {
                    if(jobj["GPUData"].GetObject().ContainsKey("GpuAdapter"))
                    {
                        foreach(var i in jobj["GPUData"].GetObject()["GpuAdapter"].GetArray())
                        {
                            var g = i.GetObject();
                            var gpu= new SystemPerf.GpuData.GpuAdapter()
                            {
                                DedicatedMemory = g.ContainsKey("DedicatedMemory") ? Convert.ToInt32(g["DedicatedMemory"].GetNumber()) : 0,
                                DedicatedMemoryUsed = g.ContainsKey("DedicatedMemoryUsed") ? Convert.ToInt32(g["DedicatedMemoryUsed"].GetNumber()) : 0,
                                SystemMemory = g.ContainsKey("SystemMemory") ? Convert.ToInt32(g["SystemMemory"].GetNumber()) : 0,
                                SystemMemoryUsed = g.ContainsKey("SystemMemoryUsed") ? Convert.ToInt32(g["SystemMemoryUsed"].GetNumber()) : 0,
                                Description = g.ContainsKey("Description") ? g["Description"].GetString() : "",
                            };
                            if(g.ContainsKey("EnginesUtilization"))
                            {
                                foreach(var e in g["EnginesUtilization"].GetArray())
                                {
                                    gpu.EnginesUtilization = new List<float>();
                                    (gpu.EnginesUtilization as List<float>).Add(Convert.ToSingle(e.GetNumber()));
                                }
                            }
                            gpus.Add(gpu);
                        }
                    }
                }
                return sysperf;
            }
            else
            {
                throw new DeviceConnectionException("Failed to connect", res.StatusCode);
            }
        }
    }
}
