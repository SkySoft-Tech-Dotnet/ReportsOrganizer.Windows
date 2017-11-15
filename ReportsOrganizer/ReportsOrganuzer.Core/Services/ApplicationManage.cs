using Microsoft.Win32;
using System.IO;
using System.Reflection;

namespace ReportsOrganizer.Core.Services
{
    public interface IApplicationManage
    {
        bool IsAutorun { get; set; }
    }

    internal class ApplicationManage : IApplicationManage
    {
        private static string AutorunRegistry = "ReportsOrganizer";
        private static byte[] AutorunEnableBin = { 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        private string CurrentPath
            => Assembly.GetEntryAssembly().Location;

        private RegistryKey AutorunRegistryKey
            => Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

        private RegistryKey AutorunInTaskManagerRegistryKey
            => Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run", true);
        
        public bool IsAutorun
        {
            get => CheckAutorun();
            set
            {
                if (value)
                {
                    EnableAutorun();
                }
                else
                {
                    DisableAutorun();
                }
            }
        }

        private bool CheckAutorun()
        {
            return AutorunRegistryKey.GetValue(AutorunRegistry) != null
                && AutorunInTaskManagerRegistryKey.GetValue(AutorunRegistry)?.Equals(AutorunEnableBin) != null;
        }

        private void EnableAutorun()
        {
            AutorunRegistryKey.SetValue(AutorunRegistry, CurrentPath);
            AutorunRegistryKey.Close();
            
            AutorunInTaskManagerRegistryKey.SetValue(AutorunRegistry, AutorunEnableBin, RegistryValueKind.Binary);
            AutorunInTaskManagerRegistryKey.Close();
        }

        private void DisableAutorun()
        {
            if (AutorunRegistryKey.GetValue(AutorunRegistry) != null)
            {
                AutorunRegistryKey.DeleteValue(AutorunRegistry);
                AutorunRegistryKey.Close();
            }

            if (AutorunInTaskManagerRegistryKey.GetValue(AutorunRegistry)?.Equals(AutorunEnableBin) != null)
            {
                AutorunInTaskManagerRegistryKey.DeleteValue(AutorunRegistry);
                AutorunRegistryKey.Close();
            }
        }
    }
}
