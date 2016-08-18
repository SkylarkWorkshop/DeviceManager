using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeviceManager.Model
{
    public class OSInfo
    {
        public string Language { get; internal set; }
        public string OsEdition { get; internal set; }
        public int OsEditionId { get; internal set; }
        public string OsVersion { get; internal set; }
        public string Platform { get; internal set; }

    }
}
