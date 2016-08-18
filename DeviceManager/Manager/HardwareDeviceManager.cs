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
    class HardwareDeviceManager
    {
        public static async Task<IList<HardwareDevice>> GetHardwareDeviceInfo(HttpClient client,string addr)
        {
            var res = await client.GetAsync(new Uri("https://" + addr + "/api/devicemanager/devices"));
            var responseText = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode == true)
            {
                List<HardwareDevice> devices = new List<HardwareDevice>();
                JsonObject jobj = JsonObject.Parse(responseText);
                JsonArray jarr = jobj["DeviceList"].GetArray();
                jarr.ToList().ForEach(i =>
                {
                    var o = i.GetObject();
                    devices.Add(new HardwareDevice()
                    {
                        Class = o.ContainsKey("Class") ? o["Class"].GetString() : "",
                        FriendlyName = o.ContainsKey("FriendlyName") ? o["FriendlyName"].GetString() : "",
                        Description = o.ContainsKey("Description") ? o["Description"].GetString() : "",
                        ID = o.ContainsKey("ID") ? o["ID"].GetString() : "",
                        Manufacturer = o.ContainsKey("Manufacturer") ? o["Manufacturer"].GetString() : "",
                        ParentID = o.ContainsKey("ParentID") ? o["ParentID"].GetString() : "",
                        ProblemCode = o.ContainsKey("ProblemCode") ? Convert.ToInt32(o["ProblemCode"].GetNumber()) : 0,
                        StatusCode = o.ContainsKey("StatusCode") ? Convert.ToInt32(o["StatusCode"].GetNumber()) : 0
                    });
                });
                return devices;
            }
            else
            {
                throw new DeviceConnectionException("Failed to connect", res.StatusCode);
            }
        }
    }
}
