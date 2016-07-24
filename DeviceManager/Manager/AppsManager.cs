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
    class AppsManager
    {
        public static async Task<IList<AppxPackage>> GetApplicationInfo(HttpClient client,string addr)
        {
            var res = await client.GetAsync(new Uri("http://" + addr + "/api/app/packagemanager/packages"));
            var responseText = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode == true)
            {
                List<AppxPackage> application = new List<AppxPackage>();
                List<RegisteredUser> rusers = new List<RegisteredUser>();
                JsonObject jobj = JsonObject.Parse(responseText);
                JsonArray jarr = jobj["InstalledPackages"].GetArray();
                jarr.ToList().ForEach(i =>
                {
                    var o = i.GetObject();
                    JsonArray jarr1 = o["RegisteredUsers"].GetArray();
                    jarr1.ToList().ForEach(_i =>
                    {
                        var _o = _i.GetObject();
                        rusers.Add(new RegisteredUser() { UserDisplayName = _o.ContainsKey("UserDisplayName") ? _o["UserDisplayName"].GetString() : "", UserSID = _o.ContainsKey("UserSID") ? _o["UserSID"].GetString() : "" });
                    }
                    );
                    application.Add(new AppxPackage() { CanUninstall = o.ContainsKey("CanUninstall") ? o["CanUninstall"].GetBoolean() : true, Name = o.ContainsKey("Name") ? o["Name"].GetString() : "", PackageFamilyName = o.ContainsKey("PackageFamilyName") ? o["PackageFamilyName"].GetString() : "", PackageFullName = o.ContainsKey("PackageFullName") ? o["PackageFullName"].GetString() : "", PackageOrigin = o.ContainsKey("PackageOrigin") ? Convert.ToInt32(o["PackageOrigin"].GetNumber()) : 0, PackageRelativeId = o.ContainsKey("PackageRelativeId") ? o["PackageRelativeId"].GetString() : "", Publisher = o.ContainsKey("Publisher") ? o["Publisher"].GetString() : "", Version = o.ContainsKey("Version") ? new Version(Convert.ToInt32(o["Version"].GetObject()["Major"].GetNumber()), Convert.ToInt32(o["Version"].GetObject()["Minor"].GetNumber()), Convert.ToInt32(o["Version"].GetObject()["Build"].GetNumber()), Convert.ToInt32(o["Version"].GetObject()["Revision"].GetNumber())) : null, RegUser = rusers });
                }
                );
				return application;
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
