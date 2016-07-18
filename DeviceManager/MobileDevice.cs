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

namespace DeviceManager
{
    public class MobileDevice
    {
        HttpClient client;
        /// <summary>
        /// Init and connect to the mobile device with the specified address.
        /// </summary>
        /// <param name="addr"></param>
        public MobileDevice(string addr)
        {
            IsReady = false;
            Address = addr;
            IsAuthed = false;
            HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
            filter.AllowAutoRedirect = false;
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted | ChainValidationResult.InvalidName);
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
        /// Get a bool value which indicates if the current the connection to the mobile device has established successfully.
        /// </summary>
        public bool IsConnected { get; private set; }
        /// <summary>
        /// Return the device of current device.
        /// </summary>
        public DeviceType DeviceType
        {
            get { return DeviceType.Mobile; }
        }
        private async void TestConnection()
        {
            try
            {
                var res=await client.GetAsync(new Uri($"http://{Address}/default.htm"));
                if(res.IsSuccessStatusCode==true)
                {
                    IsAuthed = true;
                    IsConnected= true;
                    
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
        /// Shutdown the current device.
        /// </summary>
        public async void Shutdown()
        {
            while (!this.IsReady)
            {
                await Task.Delay(5);
            }
            if (IsConnected)
            {                
                var res = await client.PostAsync(new Uri("http://" + Address + $"/api/control/shutdown"), null);
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
        /// Restart the current device.
        /// </summary>
        public async void Reboot()
        {
            while (!this.IsReady)
            {
                await Task.Delay(5);
            }
            if (IsConnected)
            {
                var res = await client.PostAsync(new Uri("http://" + Address + $"/api/control/restart"), null);
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

                var res = await client.PostAsync(new Uri("http://" + Address + $"/api/authorize/pair?pin={credential.Pin}&persistent={credential.Persistent}"), null);
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
    }
}
