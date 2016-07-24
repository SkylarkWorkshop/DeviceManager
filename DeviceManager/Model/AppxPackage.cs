﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DeviceManager.Model
{
    public class AppxPackage
    {
        public bool CanUninstall { get; set; }
        public string Name { get; set; }
        public string PackageFamilyName { get; set; }
        public string PackageFullName { get; set; }
        public int PackageOrigin { get; set; }
        public string PackageRelativeId { get; set; }
        public string Publisher { get; set; }
        public object Version { get; set; }
        public List<RegisteredUsers> ru { get; set; }
    }
    public class RegisteredUsers
    {
        public string UserDisplayName { get; set; }
        public string UserSID { get; set; }

    }
}