using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinneretCollegeTimeSheet.Data;
using KinneretCollegeTimeSheet.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace KinneretCollegeTimeSheet.Controllers
{
    public class ImportExportController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public ImportExportController(IHostingEnvironment hostingEnvironment,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostExport()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;

            string sFileName = @"Newdemo.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                IWorkbook workbook = new XSSFWorkbook(fs);
                ISheet excelSheet = workbook.GetSheet("Sheet");

                excelSheet.GetColumnWidth(0);
                int rowCount =excelSheet.LastRowNum;
                
                IRow headerRow = excelSheet.GetRow(6); //Get Header Row
                int cellCount = headerRow.LastCellNum;

                foreach (var cell in headerRow)
                {
                    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                    string x = cell.ToString();
                }

                var user = await _userManager.GetUserAsync(User);

                // get data 
                var data  = await _context.userCourse
                    .Include(u => u.Course)
                    .Include(u => u.reports)
                    .Where(m => m.UserID == user.Id)
                    .ToListAsync();

                IRow row;
                // set username
                row = excelSheet.GetRow(2);
                row.GetCell(2).SetCellValue(user.UserName);

                // set user ID
                row = excelSheet.GetRow(3);
                row.GetCell(2).SetCellValue(user.CertificateID);

                int indexRow = 7;

                foreach (var item in data)
                {
                    foreach (var report in item.reports)
                    {
                        row = excelSheet.GetRow(indexRow++);
                        row.GetCell(1).SetCellValue(item.Course.Name);
                        row.GetCell(2).SetCellValue(report.timeStart);
                        row.GetCell(3).SetCellValue(report.timeEnd);
                        row.GetCell(5).SetCellValue("כן");
                    }
                }

                sFileName = @"AttendanceReport.xlsx";
                var fsOut = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write);
                workbook.Write(fsOut);
                fs.Close();
                fsOut.Close();
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }

        [HttpPost]
        public async Task<IActionResult> OnPostImport()
        {

            List<Course> ListCourses = await _context.Course.ToListAsync();
            List<Course> NewListCourse = new List<Course>();


            IFormFile file = Request.Form.Files[0];
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }



                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;
                    sb.Append("<table class='table'><tr>");
                    for (int j = 0; j < cellCount; j++)
                    {
                        ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        sb.Append("<th>" + cell.ToString() + "</th>");
                    }
                    sb.Append("</tr>");
                    sb.AppendLine("<tr>");
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {

                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;


                        Course c = new Course
                        {
                            Name = row.GetCell(0).ToString(),
                            Key = row.GetCell(1).ToString(),
                            LecturerName = row.GetCell(2).ToString(),
                            LecturerEmail = row.GetCell(3).ToString()
                        };


                        var result = ListCourses.Find(t => t.Key.Equals(c.Key));

                        if (result != null) continue;

                        ListCourses.Add(c);
                        NewListCourse.Add(c);

                        //if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");
                        }

                    


                        sb.AppendLine("</tr>");
                    }
                    sb.Append("</table>");
                }
            }


            _context.AddRange(NewListCourse);
            await _context.SaveChangesAsync();
            return this.Content(sb.ToString());
        }
    }


}
