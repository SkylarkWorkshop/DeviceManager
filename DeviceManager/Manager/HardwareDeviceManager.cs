﻿using System;
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
    class HardwareDeviceManager
    {
        public static async Task<IList<HardwareDevice>> GetHardwareDeviceInfo(HttpClient client,string addr)
        {
            var res = await client.GetAsync(new Uri("http://" + addr + ""));
            var responseText = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode == true)
            {
                List<HardwareDevice> devices = new List<HardwareDevice>();
                JsonObject jobj = JsonObject.Parse(responseText);
                JsonArray jarr = jobj["DeviceList"].GetArray();
                jarr.ToList().ForEach(i =>
                {
                    var o = i.GetObject();
                    devices.Add(new HardwareDevice() { o.ContainsKey("Class") ? o["Class"].GetString() : "", o.ContainsKey("FriendlyName") ? o["FriendlyName"].GetString() : "", o.ContainsKey("Description") ? o["Description"].GetString() : "", o.ContainsKey("ID") ? o["ID"].GetString() : "", o.ContainsKey("Manufacturer") ? o["Manufacturer"].GetString() : "", o.ContainsKey("ParentID") ? o["ParentID"].GetString() : "", o.ContainsKey("ProblemCode") ? o["ProblemCode"].GetNumber() : 0, o.ContainsKey("StatusCode") ? o["StatusCode"].GetNumber() : 0 };
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
