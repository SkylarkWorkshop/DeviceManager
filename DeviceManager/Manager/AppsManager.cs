using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceManager.Model;
using Windows.Data.Json;
using System.Net.Http;
using static DeviceManager.DeviceManager;

namespace DeviceManager.Manager
{
    class AppsManager
    {
        HttpClient client;
        public string Address {get; set;}
        public bool IsConnected {get; set;}
        public async Task<IList<Process>> GetApplicationInf()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.AllowAutoRedirect = false;
            client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.84 Safari/537.36");
            client.Timeout = TimeSpan.FromSeconds(10);
            if (IsConnected)
            {
                var res = await client.GetAsync(new Uri("http://" + Address + $"/api/app/packagemanager/packages"));
                var responseText = await res.Content.ReadAsStringAsync();
                if (res.IsSuccessStatusCode == true)
                {
                    /*List<Process> processes = new List<Process>();
                    JsonObject jobj = JsonObject.Parse(responseText);
                    JsonArray jarr = jobj["Processes"].GetArray();
                    jarr.ToList().ForEach(i =>
                    {
                        var o = i.GetObject();
                       
                        processes.Add(new Process() { CPUUsage = o.ContainsKey("CPUUsage") ? o["CPUUsage"].GetNumber() : 0, ImageName = o.ContainsKey("ImageName") ? o["ImageName"].GetString() : "", PageFileUsage = o.ContainsKey("PageFileUsage") ? o["PageFileUsage"].GetNumber() : 0, PrivateWorkingSet = o.ContainsKey("PrivateWorkingSet") ? o["PrivateWorkingSet"].GetNumber() : 0, ProcessId = o.ContainsKey("ProcessId") ? o["ProcessId"].GetNumber() : 0, SessionId = o.ContainsKey("SessionId") ? o["SessionId"].GetNumber() : 0, UserName = o.ContainsKey("UserName") ? o["UserName"].GetString() : "", VirtualSize = o.ContainsKey("VirtualSize") ? o["VirtualSize"].GetNumber() : 0, WorkingSetSize = o.ContainsKey("WorkingSetSize") ? o["WorkingSetSize"].GetNumber() : 0, Version = o.ContainsKey("Version") ? new Version(Convert.ToInt32(o["Version"].GetObject()["Major"].GetNumber()), Convert.ToInt32(o["Version"].GetObject()["Minor"].GetNumber()), Convert.ToInt32(o["Version"].GetObject()["Build"].GetNumber()), Convert.ToInt32(o["Version"].GetObject()["Revision"].GetNumber())) : null, PackageFullName = o.ContainsKey("PackageFullName") ? o["PackageFullName"].GetString() : "", Publisher = o.ContainsKey("Publisher") ? o["Publisher"].GetString() : "", TotalCommit = o.ContainsKey("TotalCommit") ? o["TotalCommit"].GetNumber() : -1 });
                    });
                    return processes;*/
                    throw new NotImplementedException();
                }
                else
                {
                    throw new DeviceConnectionException("Failed to connect", res.StatusCode);
                }
            }
            else
            {
                throw new DeviceConnectionException("Not connected");
            }
        }
        public static async void DeployAppAsync(HttpClient client,string addr,AppxPackage app)
        {
            throw new NotImplementedException();
        }
    }
}
