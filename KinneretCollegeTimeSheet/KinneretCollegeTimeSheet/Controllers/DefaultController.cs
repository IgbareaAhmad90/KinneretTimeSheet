using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinneretCollegeTimeSheet.Data;
using Microsoft.AspNetCore.Mvc;

namespace KinneretCollegeTimeSheet.Controllers
{
    public class DefaultController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DefaultController(ApplicationDbContext context)
        {
            _context = context;
        }

    }
   
}