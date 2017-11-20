using Microsoft.Win32;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ReportsOrganizer.Core.Managers
{
    public interface IApplicationManage
    {
        bool IsAutorun { get; }
        bool IsAutorunMinimize { get; }

        void ChangeAutorun(bool enable);
        void ChangeAutorunMinimize(bool enable);
    }

    internal class ApplicationManage : IApplicationManage
    {
        private static string AutorunRegistry = "ReportsOrganizer";
        private static byte[] AutorunApprovedBin = { 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        private static string AutorunRegistryPath
            => @"Software\Microsoft\Windows\CurrentVersion\Run";

        private static string AutorunApprovedRegistryPath
            => @"Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run";

        private static Regex AutorunPathRegex => new Regex("^\"([^\"]+)\"(.*)$");

        public bool IsAutorun
        {
            get => GetAutorunValue<string>(AutorunRegistryPath) != null
                && GetAutorunValue<byte[]>(AutorunApprovedRegistryPath, new byte[] { }) .SequenceEqual(AutorunApprovedBin);
        }

        public bool IsAutorunMinimize
        {
            get => AutorunPathRegex.Match(GetAutorunValue<string>(AutorunRegistryPath, string.Empty)).Groups[2]
                .Value.Split(new char[] { ' ' }).Any(property => property.Trim() == "/minimize");
        }

        public void ChangeAutorun(bool enable)
        {
            var key = Registry.CurrentUser.OpenSubKey(AutorunRegistryPath, true);
            var keyApproved = Registry.CurrentUser.OpenSubKey(AutorunApprovedRegistryPath, true);
            if (enable)
            {
                key.SetValue(AutorunRegistry, $"\"{Assembly.GetEntryAssembly().Location}\"");
                keyApproved.SetValue(AutorunRegistry, AutorunApprovedBin, RegistryValueKind.Binary);
            }
            else
            {
                key.DeleteValue(AutorunRegistry);
                keyApproved.DeleteValue(AutorunRegistry);
            }
            key.Close();
        }

        public void ChangeAutorunMinimize(bool enable)
        {
            var key = Registry.CurrentUser.OpenSubKey(AutorunRegistryPath, true);
            key.SetValue(AutorunRegistry, enable
                ? $"\"{Assembly.GetEntryAssembly().Location}\" /minimize"
                : $"\"{Assembly.GetEntryAssembly().Location}\"");
            key.Close();
        }

        private T GetAutorunValue<T>(string path)
        {
            return GetAutorunValue<T>(path, null);
        }

        private T GetAutorunValue<T>(string path, object defaultValue)
        {
            var key = Registry.CurrentUser.OpenSubKey(path);
            var value = key.GetValue(AutorunRegistry, defaultValue);

            key.Close();
            return (T)value;
        }
    }
}
