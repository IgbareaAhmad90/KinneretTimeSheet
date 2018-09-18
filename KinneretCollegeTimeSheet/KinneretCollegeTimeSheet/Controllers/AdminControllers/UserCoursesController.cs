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
using Microsoft.AspNetCore.Authorization;

namespace KinneretCollegeTimeSheet.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserCourses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.userCourse.Include(u => u.Course).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> GetCoursesByUser(string id)
        {
            ViewData["UserID"] = id;
            return View(await _context.userCourse
                .Include(u => u.Course)
                .Include(u => u.User)
                .Where(m => m.UserID == id)
                .ToListAsync());
        }

        // GET: UserCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var userCourse = await _context.userCourse
                .Include(u => u.Course)
                .Include(u => u.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userCourse == null)
            {
                return NotFound();
            }

            return View(userCourse);
        }
        //// GET: UserCourses/Details/5
        //public async Task<IActionResult> GetDetailsByUser(int id)
        //{
        //    var userCourse = await _context.userCourse
        //        .Include(u => u.Course)
        //        .Include(u => u.User)
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (userCourse == null)
        //    {
        //        return NotFound();
        //    }

        //    return PartialView(userCourse);
        //}


        // GET: UserCourses/Create
        public IActionResult Create(string id)
        {
            ViewData["CourseID"] = new SelectList(_context.Course, "Id", "Name");
            ViewData["UserID"] = id;
            return View();
        }

        // POST: UserCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserCourseModel userCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new UserCourse { CourseID = userCourse.CourseID, UserID = userCourse.UserID });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(userCourse);
        }

        // GET: UserCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userCourse = await _context.userCourse.SingleOrDefaultAsync(m => m.Id == id);
            if (userCourse == null)
            {
                return NotFound();
            }
            ViewData["CourseID"] = new SelectList(_context.Course, "Id", "LecturerEmail", userCourse.CourseID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", userCourse.UserID);
            return View(userCourse);
        }

        // POST: UserCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserID,CourseID")] UserCourse userCourse)
        {
            if (id != userCourse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCourseExists(userCourse.Id))
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
            ViewData["CourseID"] = new SelectList(_context.Course, "Id", "LecturerEmail", userCourse.CourseID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", userCourse.UserID);
            return View(userCourse);
        }

        // GET: UserCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCourse = await _context.userCourse
                .Include(u => u.Course)
                .Include(u => u.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userCourse == null)
            {
                return NotFound();
            }

            return View(userCourse);
        }

        // POST: UserCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userCourse = await _context.userCourse.SingleOrDefaultAsync(m => m.Id == id);
            _context.userCourse.Remove(userCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCourseExists(int id)
        {
            return _context.userCourse.Any(e => e.Id == id);
        }
    }
}
