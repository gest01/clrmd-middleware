using System.Runtime.InteropServices;

namespace Diagnostics.Runtime.Middleware
{
    public static class PlatformServices
    {
        public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        public static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }
}
