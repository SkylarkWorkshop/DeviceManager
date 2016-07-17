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
        public static async Task<IList<Process>> GetApplicationInfo(HttpClient client,string addr)
        {
            var res = await client.GetAsync(new Uri("http://" + addr + $"/api/app/packagemanager/packages"));
            var responseText = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode == true)
            {
                List<Application> application = new List<Applicatoin>();
                JsonObject jobj = JsonObject.Parse(responseText);
                throw new NotImplementedException();
            }
            else
            {
                throw new DeviceConnectionException("Failed to connect", res.StatusCode);
            }
        }
        public static async void DeployAppAsync(HttpClient client,string addr,AppxPackage app)
        {
            throw new NotImplementedException();
        }
    }
}
