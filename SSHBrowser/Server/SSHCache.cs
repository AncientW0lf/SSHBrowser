using SSHClient;

namespace SSHBrowser.Server
{
    internal static class SSHCache
    {
        public static Client Client { get; set; }

        public static void Dispose()
        {
            Client?.Dispose();
        }
    }
}