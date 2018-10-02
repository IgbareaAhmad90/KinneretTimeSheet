using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KinneretCollegeTimeSheet.Data;
using KinneretCollegeTimeSheet.Models;
using KinneretCollegeTimeSheet.Models.RegistrationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace KinneretCollegeTimeSheet.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReportsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Reports
        public async Task<IActionResult> Index(int? Id)
        {

            ViewData["Id"] = Id;

            bool isAdmin = User.IsInRole("Admin");
            if (isAdmin)
            {
                return View(await _context.Report.Where(r => r.UserCourseId == Id).ToListAsync());
            }

            // get User
            var user = await _userManager.GetUserAsync(User);
            // check if the Course About me 
            var userCourse = _context.userCourse.Where(m => m.Id == Id && m.User.Id == user.Id);
            if(userCourse.Count() == 0)
            {
                //TODO Create ErrorPage
                return NotFound();
            }

            return View(await _context.Report.Where(r => r.UserCourseId == Id).ToListAsync());
        }

        //    [Authorize(Roles = "Admin")]
        //// GET: Reports
        //public async Task<IActionResult> Index(int? Id)
        //{
        //    ViewData["Id"] = Id;
        //    var applicationDbContext = _context.Report
        //        .Where(r => r.UserCourseId == Id);
        //    return View(await applicationDbContext.ToListAsync());
        //}


        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .Include(r => r.UserCourse)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create(int id)
        {
            ViewData["UserCourseId"] = id;
            return View();
        }

        public bool CheckHafefa(int ts1,int tf1, int ts2 ,int tf2) {


            if (ts2 - tf1 < 0 || ts1 - tf2 < 0)
                return false;

            return true ;
        }


        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,type,date,timeStart,timeEnd,discription,UserCourseId")] Report report)
        {
            //TODO Check if have Report with the same time 
            var user = await _userManager.GetUserAsync(User);



            var list = _context.Report
                 .Where(d => d.date == report.date)
                 .Where(u => u.UserCourse.UserID == user.Id)
                 .Where(t=> (t.timeStart.Hour - report.timeEnd.Hour <0) && (report.timeStart.Hour -t.timeEnd.Hour  <0) );

            if (list.Count() > 0)
            {
                return NotFound();
            }

            report.Id = 0;
            if (ModelState.IsValid)
            {
                _context.Add(report);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Reports", action = "Index", Id = report.UserCourseId }));
            }
            return View(report);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report.SingleOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["CourseID"] = new SelectList(_context.Course, "Id", "LecturerEmail", report.UserCourseId);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,date,timeStart,timeEnd,discription,UserCourseId")] Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Course, "Id", "LecturerEmail", report.UserCourseId);
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .Include(r => r.UserCourse)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Report.SingleOrDefaultAsync(m => m.Id == id);
            _context.Report.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Report.Any(e => e.Id == id);
        }
    }
}
