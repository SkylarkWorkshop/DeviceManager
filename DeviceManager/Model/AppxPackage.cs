using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.Model
{
    public class AppxPackage
    {
        public bool IsXap { get; set; }
        public string Name { get; set; }
        public string PackageFamilyName { get; set; }
        public string PackageFullName { get; set; }
        public int PackageOrigin { get; set; }
        public string PackageRelativeId { get; set; }
    }
}
