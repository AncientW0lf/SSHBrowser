using System.IO;
using System;
using Renci.SshNet;

namespace SSHClient
{
    public class Client : IDisposable
    {
        private readonly SshClient _ssh;

        private readonly ScpClient _scp;

        public Client(string host, string user, string password)
        {
            _ssh = new SshClient(host, user, password);
            _scp = new ScpClient(host, user, password);
            _ssh.Connect();
            _scp.Connect();
        }

        public string[] ListFiles(string dir, bool showHidden = false)
        {
            return _ssh
                .RunCommand($"ls{(showHidden ? " -A" : "")} \"{dir}\"")
                .Result.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }

        public Stream DownloadFile(string file)
        {
            var s = new MemoryStream();

            _scp.Download(file, s);

            return s;
        }

        public void Dispose()
        {
            _ssh.Dispose();
        }
    }
}