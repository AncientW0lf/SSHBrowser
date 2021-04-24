using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SSHBrowser.Server;

namespace SSHBrowser.Pages
{
    public class IndexModel : PageModel
    {
        public string DirPath { get; private set; } = "/";

        public string[] Files { get; private set; } = new string[0];

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(string p)
        {
            if (SSHCache.Client == null)
            {
                Response.Redirect("/connect");
                return;
            }

            if (!string.IsNullOrWhiteSpace(p))
                DirPath = p;

            Files = SSHCache.Client.GetFiles(DirPath);
        }
    }
}
