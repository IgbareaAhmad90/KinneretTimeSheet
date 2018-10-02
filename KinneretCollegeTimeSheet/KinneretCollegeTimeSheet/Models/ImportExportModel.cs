using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinneretCollegeTimeSheet.Models
{
    public class ImportExportModel
    {

        private IHostingEnvironment _hostingEnvironment;
        public ImportExportModel(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
    }
}
