namespace MovieZone.ApiControllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class MetaController : ControllerBase
    {
        [HttpGet("/info")]
        public IActionResult Info()
        {
            var assembly = typeof(Startup).Assembly;

            var lastUpdate = System.IO.File.GetLastWriteTime(assembly.Location);
            var version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

            return this.Ok($"Version: {version}, Last Updated: {lastUpdate}");
        }
    }
}
