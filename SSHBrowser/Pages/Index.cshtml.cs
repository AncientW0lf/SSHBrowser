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

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(string p)
        {
            if (SSHCache.Client == null)
                Response.Redirect("/connect");

            if (!string.IsNullOrWhiteSpace(p))
                DirPath = p;
        }
    }
}
