using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinneretCollegeTimeSheet.Data;
using KinneretCollegeTimeSheet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KinneretCollegeTimeSheet.Controllers
{
    public class PartialViewController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public PartialViewController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }



        [Authorize]
        public async Task<IActionResult> MyCourses()
        {

            var userID = await _userManager.GetUserAsync(User);

            return View(await _context.userCourse
                         .Include(u => u.Course)
                         .Include(u => u.User)
                         .Where(m => m.UserID == userID.Id)
                         .ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> MyProfile()
        {

            var user = await _userManager.GetUserAsync(User);

            var userCourse = await _context.Users
                .Include(b => b.UserCourses)
                .SingleOrDefaultAsync(m => m.Id == user.Id);
            if (userCourse == null)
            {
                return NotFound();
            }

            return View(userCourse);
        }





    }
}