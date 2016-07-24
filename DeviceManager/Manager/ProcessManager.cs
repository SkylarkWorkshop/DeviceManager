using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceManager.Model;
using Windows.Data.Json;
using Windows.Web.Http;
using static DeviceManager.DeviceManager;

namespace DeviceManager.Manager
{
    class ProcessManager
    {
        public static async Task<IList<Process>> GetProcessesInfoForIoTDeviceAsync(HttpClient client, string addr)
        {
            var res = await client.GetAsync(new Uri("http://" + addr + "/api/resourcemanager/processes"));
            var responseText = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode == true)
            {
                List<Process> processes = new List<Process>();
                JsonObject jobj = JsonObject.Parse(responseText);
                JsonArray jarr = jobj["Processes"].GetArray();
                jarr.ToList().ForEach(i =>
                {
                    var o = i.GetObject();
                    processes.Add(new Process() { CPUUsage = o.ContainsKey("CPUUsage") ? o["CPUUsage"].GetNumber() : 0, ImageName = o.ContainsKey("ImageName") ? o["ImageName"].GetString() : "", PageFileUsage = o.ContainsKey("PageFileUsage") ? o["PageFileUsage"].GetNumber() : 0, PrivateWorkingSet = o.ContainsKey("PrivateWorkingSet") ? o["PrivateWorkingSet"].GetNumber() : 0, ProcessId = o.ContainsKey("ProcessId") ? o["ProcessId"].GetNumber() : 0, SessionId = o.ContainsKey("SessionId") ? o["SessionId"].GetNumber() : 0, UserName = o.ContainsKey("UserName") ? o["UserName"].GetString() : "", VirtualSize = o.ContainsKey("VirtualSize") ? o["VirtualSize"].GetNumber() : 0, WorkingSetSize = o.ContainsKey("WorkingSetSize") ? o["WorkingSetSize"].GetNumber() : 0, Version = o.ContainsKey("Version") ? new Version(Convert.ToInt32(o["Version"].GetObject()["Major"].GetNumber()), Convert.ToInt32(o["Version"].GetObject()["Minor"].GetNumber()), Convert.ToInt32(o["Version"].GetObject()["Build"].GetNumber()), Convert.ToInt32(o["Version"].GetObject()["Revision"].GetNumber())) : null, PackageFullName = o.ContainsKey("PackageFullName") ? o["PackageFullName"].GetString() : "", Publisher = o.ContainsKey("Publisher") ? o["Publisher"].GetString() : "", TotalCommit = o.ContainsKey("TotalCommit") ? o["TotalCommit"].GetNumber() : -1 });
                });
                return processes;
            }
            else
            {
                throw new DeviceConnectionException("Failed to connect", res.StatusCode);
            }
        }
        public static async Task<IList<Process>> GetProcessesInfoForMobileDeviceAsync(HttpClient client, string addr)
        {
            var res = await client.GetAsync(new Uri("http://" + addr + $"/api/resourcemanager/processes"));
            var responseText = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode == true)
            {
                List<Process> processes = new List<Process>();
                JsonObject jobj = JsonObject.Parse(responseText);
                JsonArray jarr = jobj["Processes"].GetArray();
                jarr.ToList().ForEach(i =>
                {
                    var o = i.GetObject();
                    processes.Add(new Process() { CPUUsage = o.ContainsKey("CPUUsage") ? o["CPUUsage"].GetNumber() : 0, ImageName = o.ContainsKey("ImageName") ? o["ImageName"].GetString() : "", PageFileUsage = o.ContainsKey("PageFileUsage") ? o["PageFileUsage"].GetNumber() : 0, PrivateWorkingSet = o.ContainsKey("PrivateWorkingSet") ? o["PrivateWorkingSet"].GetNumber() : 0, ProcessId = o.ContainsKey("ProcessId") ? o["ProcessId"].GetNumber() : 0, SessionId = o.ContainsKey("SessionId") ? o["SessionId"].GetNumber() : 0, UserName = o.ContainsKey("UserName") ? o["UserName"].GetString() : "", VirtualSize = o.ContainsKey("VirtualSize") ? o["VirtualSize"].GetNumber() : 0, WorkingSetSize = o.ContainsKey("WorkingSetSize") ? o["WorkingSetSize"].GetNumber() : 0, Version = o.ContainsKey("Version") ? o["Version"].GetString() : "", PackageFullName = o.ContainsKey("PackageFullName") ? o["PackageFullName"].GetString() : "", Publisher = o.ContainsKey("Publisher") ? o["Publisher"].GetString() : "", TotalCommit = o.ContainsKey("TotalCommit") ? o["TotalCommit"].GetNumber() : -1 });
                });
                return processes;
            }
            else
            {
                throw new DeviceConnectionException("Failed to connect", res.StatusCode);
            }
        }
        
    
    }
}
