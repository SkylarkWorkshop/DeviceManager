using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web;
using System.Threading;
using Windows.Data.Json;
using static DeviceManager.DeviceManager;
using Windows.Web.Http.Filters;
using Windows.Security.Cryptography.Certificates;
using DeviceManager.Model;
using DeviceManager.Manager;

namespace DeviceManager
{
    public class MobileDevice:IDevice
    {
        HttpClient client;
        HttpBaseProtocolFilter filter;
        /// <summary>
        /// Init and connect to the mobile device with specified address.
        /// </summary>
        /// <param name="addr"></param>
        public MobileDevice(string addr)
        {
            IsReady = false;
            Address = addr;
            IsAuthed = false;
            HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
            filter.AllowAutoRedirect = false;
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Expired);
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.InvalidName);
            client = new HttpClient(filter);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.84 Safari/537.36");
            TestConnection();      
        }
        public bool IsReady { get; private set; }
        /// <summary>
        /// Get the address of current MobileDevice instance.
        /// </summary>
        public string Address
        {
            get;
        }
        /// <summary>
        /// Get a bool value which indicates if the current MobileDevice has authenticated.
        /// </summary>
        public bool IsAuthed { get; private set; }
        /// <summary>
        /// Get a bool value which indicates if the connection to the mobile device has established successfully.
        /// </summary>
        public bool IsConnected { get; private set; }
        /// <summary>
        /// Get the device type of current device.
        /// </summary>
        public DeviceType DeviceType
        {
            get { return DeviceType.Mobile; }
        }
        private async void TestConnection()
        {
            try
            {
                var res = await client.GetAsync(new Uri($"https://{Address}/default.htm"));
                if (filter != null)
                {
                    var cookies = filter.CookieManager.GetCookies(new Uri($"https://{Address}/default.htm")).Where(x => x.Name == "CSRF-Token");
                    foreach (var i in cookies)
                    {
                        client.DefaultRequestHeaders.Add("X-CSRF-Token", i.Value);
                    }
                }

                if (res.IsSuccessStatusCode == true)
                {
                    IsAuthed = true;
                    IsConnected = true;
                }
                else
                {
                    if (res.StatusCode == HttpStatusCode.TemporaryRedirect)
                    {
                        IsAuthed = false;
                        IsConnected = true;

                    }
                    else
                    {
                        IsAuthed = false;
                        IsConnected = false;
                    }

                }

                IsReady = true;
            }
            catch
            {
                IsAuthed = false;
                IsConnected = false;
                IsReady = true;
            }
        }
        /// <summary>
        /// Shutdown current device.
        /// </summary>
        public async void Shutdown()
        {
            while (!this.IsReady)
            {
                await Task.Delay(5);
            }
            if (IsConnected)
            {                
                var res = await client.PostAsync(new Uri("https://" + Address + $"/api/control/shutdown"), null);
                if (res.IsSuccessStatusCode == false)
                {
                    if (res.StatusCode == HttpStatusCode.TemporaryRedirect)
                    {
                        IsAuthed = false;
                    }
                    throw new DeviceConnectionException("Failed to connect.", res.StatusCode);
                }
            }
            else
            {
                throw new DeviceConnectionException("Not connected");
            }  
        }
        /// <summary>
        /// Restart current device.
        /// </summary>
        public async void Reboot()
        {
            while (!this.IsReady)
            {
                await Task.Delay(5);
            }
            if (IsConnected)
            {
                var res = await client.PostAsync(new Uri("https://" + Address + $"/api/control/restart"), null);
                if (res.IsSuccessStatusCode == false)
                {
                    if (res.StatusCode == HttpStatusCode.TemporaryRedirect)
                    {
                        IsAuthed = false;
                    }
                    throw new DeviceConnectionException("Failed to connect.", res.StatusCode);
                }
            }
            else
            {
                throw new DeviceConnectionException("Not connected");
            }
        }
        /// <summary>
        /// Authenticate with the specified credential.
        /// </summary>
        /// <param name="auth"></param>
        public async void Auth(ConnectCredential credential)
        {
            try
            {

                var res = await client.PostAsync(new Uri("https://" + Address + $"/api/authorize/pair?pin={credential.Pin}&persistent={credential.Persistent}"), null);
                if (res.IsSuccessStatusCode == true)
                {
                    IsAuthed = true;
                    IsConnected = true;                                       
                }
                else
                {
                    IsAuthed = false;
                    throw new DeviceConnectionException("Failed to auth.", res.StatusCode);
                }

            }
            catch
            {
                IsAuthed = false;                
            }
            
        }
        /// <summary>
        /// Get processes information of current device.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Process>> GetProcessesInfoAsync()
        {
            return await ProcessManager.GetProcessesInfoForMobileDeviceAsync(this.client, this.Address);
        }
        /// <summary>
        /// Get installed apps' information of current device.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<AppxPackage>> GetAppsInfoAsync()
        {
            return await AppsManager.GetApplicationInfo(this.client, this.Address);
        }
        /// <summary>
        /// Uninstall the app with specified packageName from current device.
        /// </summary>
        /// <param name="packageName">Package Name</param>
        /// <returns></returns>
        public async Task UninstallAppAsync(string packageName)
        {
            await AppsManager.UninstallAppAsync(this.client, this.Address, packageName);
        }
        /// <summary>
        /// Launch the app with specified appid and packageName
        /// </summary>
        /// <param name="appid">App Id</param>
        /// <param name="packageName">Package Name</param>
        /// <returns></returns>
        public async Task LaunchAppAsync(string appid, string packageName)
        {
            await AppsManager.LaunchAppAsync(this.client, this.Address,appid, packageName);
        }

        public async Task<SystemPerf> GetSystemPerfAsync()
        {
            return await SysPerfManager.GetSystemPerfAsync(this.client, this.Address);
        }
    }
}